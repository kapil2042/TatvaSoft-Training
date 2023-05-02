using CI_Platform.Models;
using CI_Platform.Models.ViewModels;
using CI_Platform.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CI_PlatformWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class AMissionController : Controller
    {
        private readonly IAdminMissionRepository _adminMissionRepository;
        private readonly ICommonRepository _commonRepository;

        public AMissionController(IAdminMissionRepository adminMissionRepository, ICommonRepository commonRepository)
        {
            _adminMissionRepository = adminMissionRepository;
            _commonRepository = commonRepository;
        }

        public IActionResult Index(string id, int pg)
        {
            if (id == null)
            {
                id = "";
            }
            TempData["pg"] = pg;
            if (pg < 1)
            {
                pg = 1;
            }
            const int pageSize = 10;
            int recsCount = _adminMissionRepository.GetTotalMissionsRecord(id);
            var pager = new VMPager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var missionData = _adminMissionRepository.GetMissions(id, recSkip, pager.PageSize);
            if (missionData.Count() == 0 && pg > 1)
            {
                return RedirectToAction("Index", "AMission", new { Area = "Admin", id = id, pg = Convert.ToInt32(TempData["pg"]) - 1 });
            }
            if (TempData["msg"] != null)
                ViewBag.success = TempData["msg"];
            ViewBag.pager = pager;
            ViewBag.query = id;
            return View(missionData);
        }

        public IActionResult AddMission()
        {
            VMAdminMission vmMission = new VMAdminMission();
            vmMission.MissionSkills = new List<MissionSkill>();
            vmMission.country = _commonRepository.GetCountriesByNotDeleted();
            vmMission.themes = _commonRepository.GetMissionThemesByNotDeleted();
            vmMission.skills = _commonRepository.GetSkills();
            return View(vmMission);
        }

        [HttpPost]
        public IActionResult AddMission(VMAdminMission vmAdminMission, int[] missionSkills, IFormFileCollection myfile, IFormFileCollection? mydocs)
        {
            vmAdminMission.MissionSkills = new List<MissionSkill>();
            if (vmAdminMission.MissionType == "TIME")
            {
                ModelState.Remove("GoalValue");
                ModelState.Remove("GoalObjectiveText");
            }
            if (missionSkills.Length > 0)
                ModelState.Remove("MissionSkills");
            else
                ViewBag.skillerr = "Please Add at least one Skill";
            if (myfile.Count() > 0)
            {
                ModelState.Remove("country");
                ModelState.Remove("themes");
                ModelState.Remove("skills");
                ModelState.Remove("mDocuments");
                ModelState.Remove("missionMedia");
            }
            else
                ViewBag.fileerr = "Please upload at least one Image";
            if (ModelState.IsValid)
            {
                Mission mission = new Mission();
                mission.Title = vmAdminMission.Title;
                mission.Description = WebUtility.HtmlEncode(vmAdminMission.Description);
                mission.ShortDescription = vmAdminMission.ShortDescription;
                mission.TotalSeat = vmAdminMission.TotalSeat;
                mission.CountryId = vmAdminMission.CountryId;
                mission.CityId = vmAdminMission.CityId;
                mission.ThemeId = vmAdminMission.ThemeId;
                mission.StartDate = vmAdminMission.StartDate;
                mission.EndDate = vmAdminMission.EndDate;
                mission.MissionType = vmAdminMission.MissionType;
                mission.Status = vmAdminMission.Status;
                mission.OrganizationName = vmAdminMission.OrganizationName;
                mission.OrganizationDetails = WebUtility.HtmlEncode(vmAdminMission.OrganizationDetails);
                mission.Availability = vmAdminMission.Availability;
                if (vmAdminMission.MissionType == "GOAL")
                {
                    GoalMission goalMission = new GoalMission();
                    goalMission.GoalValue = vmAdminMission.GoalValue;
                    goalMission.GoalObjectiveText = vmAdminMission.GoalObjectiveText;
                    mission.GoalMissions.Add(goalMission);
                }
                foreach (var i in missionSkills)
                {
                    MissionSkill missionSkill = new MissionSkill();
                    missionSkill.SkillId = i;
                    mission.MissionSkills.Add(missionSkill);
                    vmAdminMission.MissionSkills.Add(missionSkill);
                }
                foreach (IFormFile file in myfile)
                {
                    if (file != null)
                    {
                        string ImageName = Guid.NewGuid().ToString();

                        string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/missionimages", ImageName + Path.GetExtension(file.FileName));

                        using (var stream = new FileStream(SavePath, FileMode.Create))
                        {
                            MissionMedium missionMedia = new MissionMedium();
                            missionMedia.MediaType = Path.GetExtension(file.FileName);
                            missionMedia.MediaPath = "missionimages";
                            missionMedia.MediaName = ImageName;
                            mission.MissionMedia.Add(missionMedia);
                            file.CopyTo(stream);
                        }
                    }
                }
                foreach (IFormFile doc in mydocs)
                {
                    if (doc != null)
                    {
                        string DocName = Path.GetFileNameWithoutExtension(doc.FileName).ToString() + DateTime.Now.ToString("ddMMyyyyhhmmss");
                        string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/doc/missiondocuments", DocName + Path.GetExtension(doc.FileName));
                        using (var stream = new FileStream(SavePath, FileMode.Create))
                        {
                            MissionDocument missiondocs = new MissionDocument();
                            var datatype = doc.ContentType.Split('/')[1].ToLower();
                            missiondocs.DocumentType = Path.GetExtension(doc.FileName);
                            missiondocs.DocumentName = DocName;
                            missiondocs.DocumentPath = "missiondocuments";
                            mission.MissionDocuments.Add(missiondocs);
                            doc.CopyTo(stream);
                        }
                    }
                }
                _adminMissionRepository.InsertMission(mission);
                _commonRepository.Save();
                TempData["msg"] = "Record Inserted Successfully!";
                return RedirectToAction("Index", "AMission", new { Area = "Admin", pg = TempData["pg"] });
            }
            vmAdminMission.country = _commonRepository.GetCountriesByNotDeleted();
            vmAdminMission.themes = _commonRepository.GetMissionThemesByNotDeleted();
            vmAdminMission.skills = _commonRepository.GetSkills();
            return View(vmAdminMission);
        }

        public IActionResult EditMission(long id)
        {
            VMAdminMission vmAdminMission = new VMAdminMission();
            vmAdminMission.MissionSkills = _adminMissionRepository.GetSkillByMissionId(id);
            Mission mission = _adminMissionRepository.GetMissionById(id);
            vmAdminMission.Title = mission.Title;
            vmAdminMission.Description = WebUtility.HtmlDecode(mission.Description);
            vmAdminMission.ShortDescription = mission.ShortDescription;
            vmAdminMission.TotalSeat = mission.TotalSeat;
            vmAdminMission.CountryId = mission.CountryId;
            vmAdminMission.CityId = mission.CityId;
            vmAdminMission.ThemeId = mission.ThemeId;
            vmAdminMission.StartDate = mission.StartDate;
            vmAdminMission.EndDate = mission.EndDate;
            vmAdminMission.MissionType = mission.MissionType;
            vmAdminMission.Status = (int)mission.Status;
            vmAdminMission.OrganizationName = mission.OrganizationName;
            vmAdminMission.OrganizationDetails = WebUtility.HtmlDecode(mission.OrganizationDetails);
            vmAdminMission.Availability = mission.Availability;
            vmAdminMission.country = _commonRepository.GetCountries();
            vmAdminMission.themes = _commonRepository.GetMissionThemes();
            vmAdminMission.skills = _commonRepository.GetSkills();
            vmAdminMission.mDocuments = _adminMissionRepository.GetMissionDocumentsByMissionId(id);
            vmAdminMission.missionMedia = _adminMissionRepository.GetMissionMediaByMissionId(id);
            if (mission.MissionType == "GOAL")
            {
                GoalMission goalMission = _adminMissionRepository.getGoalMissionByMissionId(id);
                vmAdminMission.GoalValue = goalMission.GoalValue;
                vmAdminMission.GoalObjectiveText = goalMission.GoalObjectiveText;
            }
            return View(vmAdminMission);
        }

        [HttpPost]
        public IActionResult EditMission(long id, VMAdminMission vmAdminMission, int[] missionSkills, IFormFileCollection myfile, IFormFileCollection? mydocs, string[] preloaded)
        {
            if (vmAdminMission.MissionType == "TIME")
            {
                ModelState.Remove("GoalValue");
                ModelState.Remove("GoalObjectiveText");
            }
            if (missionSkills.Length > 0)
                ModelState.Remove("MissionSkills");
            else
                ViewBag.skillerr = "Please Add at least one Skill";
            if (myfile.Count() > 0 || preloaded.Length > 0)
            {
                ModelState.Remove("country");
                ModelState.Remove("themes");
                ModelState.Remove("skills");
                ModelState.Remove("mDocuments");
                ModelState.Remove("missionMedia");
            }
            else
                ViewBag.fileerr = "Please upload at least one Image";
            if (_commonRepository.isCountryDeleted(vmAdminMission.CountryId))
            {
                if (_commonRepository.isCityDeleted(vmAdminMission.CityId))
                {
                    if (_commonRepository.isThemeDeleted(vmAdminMission.ThemeId))
                    {
                        if (ModelState.IsValid)
                        {
                            Mission mission = _adminMissionRepository.GetMissionById(id);
                            mission.Title = vmAdminMission.Title;
                            mission.Description = WebUtility.HtmlEncode(vmAdminMission.Description);
                            mission.ShortDescription = vmAdminMission.ShortDescription;
                            mission.TotalSeat = vmAdminMission.TotalSeat;
                            mission.CountryId = vmAdminMission.CountryId;
                            mission.CityId = vmAdminMission.CityId;
                            mission.ThemeId = vmAdminMission.ThemeId;
                            mission.StartDate = vmAdminMission.StartDate;
                            mission.EndDate = vmAdminMission.EndDate;
                            mission.MissionType = vmAdminMission.MissionType;
                            mission.Status = vmAdminMission.Status;
                            mission.OrganizationName = vmAdminMission.OrganizationName;
                            mission.OrganizationDetails = WebUtility.HtmlEncode(vmAdminMission.OrganizationDetails);
                            mission.Availability = vmAdminMission.Availability;
                            if (vmAdminMission.MissionType == "GOAL")
                            {
                                GoalMission goalMission = _adminMissionRepository.getGoalMissionByMissionId(id);
                                goalMission.GoalValue = vmAdminMission.GoalValue;
                                goalMission.GoalObjectiveText = vmAdminMission.GoalObjectiveText;
                                _adminMissionRepository.UpdateGoalMission(goalMission);
                            }
                            var SkillForAdd = missionSkills.Except(mission.MissionSkills.Select(x => x.SkillId).ToArray());
                            var SkillForDelete = mission.MissionSkills.Select(x => x.SkillId).ToArray().Except(missionSkills);
                            foreach (var i in SkillForDelete)
                            {
                                _adminMissionRepository.RemoveMissionSkillsBySkillIdAndMissionId(i, id);
                            }
                            foreach (var i in SkillForAdd)
                            {
                                MissionSkill missionSkillNew = new MissionSkill();
                                missionSkillNew.MissionId = id;
                                missionSkillNew.SkillId = i;
                                mission.MissionSkills.Add(missionSkillNew);
                            }
                            List<string> listForDelete = new List<string>();
                            for (int i = 0; i < preloaded.Length; i++)
                            {
                                var x = preloaded[i].Split('/')[3];
                                var y = x.Split('.')[0];
                                listForDelete.Add(y);
                            }
                            string[] str = listForDelete.ToArray();
                            var missionMediaForDelete = vmAdminMission.missionMedia.Select(x => x.MediaName).ToArray().Except(str);
                            foreach (var i in missionMediaForDelete)
                            {
                                var img = _adminMissionRepository.GetMissionMediaByMediaName(i);
                                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/missionimages", img.MediaName + img.MediaType);
                                if (System.IO.File.Exists(imagePath))
                                {
                                    System.IO.File.Delete(imagePath);
                                }
                                _adminMissionRepository.DeleteMissionImage(img);
                            }
                            foreach (IFormFile file in myfile)
                            {
                                if (file != null)
                                {
                                    string ImageName = Guid.NewGuid().ToString();

                                    string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/missionimages", ImageName + Path.GetExtension(file.FileName));

                                    using (var stream = new FileStream(SavePath, FileMode.Create))
                                    {
                                        MissionMedium missionMedia = new MissionMedium();
                                        missionMedia.MediaType = Path.GetExtension(file.FileName);
                                        missionMedia.MediaPath = "missionimages";
                                        missionMedia.MediaName = ImageName;
                                        mission.MissionMedia.Add(missionMedia);
                                        file.CopyTo(stream);
                                    }
                                }
                            }
                            if (mydocs.Count() > 0)
                            {
                                foreach (var doc in vmAdminMission.mDocuments)
                                {
                                    string docPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/doc/missiondocuments", doc.DocumentName + doc.DocumentType);
                                    if (System.IO.File.Exists(docPath))
                                    {
                                        System.IO.File.Delete(docPath);
                                    }
                                    _adminMissionRepository.DeleteMissionDoc(doc);
                                }
                                foreach (IFormFile doc in mydocs)
                                {
                                    if (doc != null)
                                    {
                                        string DocName = Path.GetFileNameWithoutExtension(doc.FileName).ToString() + DateTime.Now.ToString("ddMMyyyyhhmmss");
                                        string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/doc/missiondocuments", DocName + Path.GetExtension(doc.FileName));
                                        using (var stream = new FileStream(SavePath, FileMode.Create))
                                        {
                                            MissionDocument missiondocs = new MissionDocument();
                                            var datatype = doc.ContentType.Split('/')[1].ToLower();
                                            missiondocs.DocumentType = Path.GetExtension(doc.FileName);
                                            missiondocs.DocumentName = DocName;
                                            missiondocs.DocumentPath = "missiondocuments";
                                            mission.MissionDocuments.Add(missiondocs);
                                            doc.CopyTo(stream);
                                        }
                                    }
                                }
                            }
                            _adminMissionRepository.UpdateMission(mission);
                            _commonRepository.Save();
                            TempData["msg"] = "Mission Updated Successfully!";
                            return RedirectToAction("Index", "AMission", new { Area = "Admin", pg = TempData["pg"] });
                        }
                    }
                    else
                    {
                        ViewBag.error = "Selected Theme is removed by Admin! Please select another theme";
                    }
                }
                else
                {
                    ViewBag.error = "Selected City is removed by Admin! Please select another city";
                }
            }
            else
            {
                ViewBag.error = "Selected Country is removed by Admin! Please select another country";
            }
            vmAdminMission.country = _commonRepository.GetCountries();
            vmAdminMission.themes = _commonRepository.GetMissionThemes();
            vmAdminMission.skills = _commonRepository.GetSkills();
            vmAdminMission.mDocuments = _adminMissionRepository.GetMissionDocumentsByMissionId(id);
            vmAdminMission.missionMedia = _adminMissionRepository.GetMissionMediaByMissionId(id);
            vmAdminMission.MissionSkills = _adminMissionRepository.GetSkillByMissionId(id);
            return View(vmAdminMission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteMission(long id)
        {
            var missionMedia = _adminMissionRepository.GetMissionMediaByMissionId(id);
            var missionDoc = _adminMissionRepository.GetMissionDocumentsByMissionId(id);
            foreach (var media in missionMedia)
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/missionimages", media.MediaName + media.MediaType);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                _adminMissionRepository.DeleteMissionImage(media);
            }
            foreach (var doc in missionDoc)
            {
                string docPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/doc/missiondocuments", doc.DocumentName + doc.DocumentType);
                if (System.IO.File.Exists(docPath))
                {
                    System.IO.File.Delete(docPath);
                }
                _adminMissionRepository.DeleteMissionDoc(doc);
            }
            _adminMissionRepository.DeleteFavouriteMissionByMissionId(id);
            _adminMissionRepository.DeleteMissionRatingByMissionId(id);
            _adminMissionRepository.DeleteMissionInviteByMissionId(id);
            _adminMissionRepository.DeleteMissionApplicationByMissionId(id);
            _adminMissionRepository.DeleteMissionSkillsByMissionId(id);
            _adminMissionRepository.DeleteTimeSheetByMissionId(id);
            _adminMissionRepository.DeleteCommentsByMissionId(id);
            _adminMissionRepository.DeleteStoriesByMissionId(id);
            var mission = _adminMissionRepository.GetMissionById(id);
            if (mission != null)
            {
                if (mission.MissionType.Equals("GOAL"))
                {
                    _adminMissionRepository.DeleteGoalMission(_adminMissionRepository.getGoalMissionByMissionId(id));
                }
                _adminMissionRepository.DeleteMission(mission);
            }
            _commonRepository.Save();
            TempData["msg"] = "Record Deleted Successfully!";
            return RedirectToAction("Index", "AMission", new { Area = "Admin", pg = TempData["pg"] });
        }
    }
}
