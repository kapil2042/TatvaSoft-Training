using CI_Platform.Models;
using CI_Platform.Models.ViewModels;
using CI_Platform.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class MissionSkillsController : Controller
    {
        private readonly IAdminMissionSkillsRepository _adminMissionSkillsRepository;
        private readonly ICommonRepository _commonRepository;

        public MissionSkillsController(IAdminMissionSkillsRepository missionSkillsRepository, ICommonRepository commonRepository)
        {
            _adminMissionSkillsRepository = missionSkillsRepository;
            _commonRepository = commonRepository;
        }

        public IActionResult Index(int pg)
        {
            TempData["pg"] = pg;
            if (pg < 1)
            {
                pg = 1;
            }
            const int pageSize = 10;
            int recsCount = _adminMissionSkillsRepository.GetTotalSkillsRecord();
            var pager = new VMPager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var MissionSkillData = _adminMissionSkillsRepository.GetSkills(recSkip, pager.PageSize);
            if (MissionSkillData.Count() == 0 && pg > 1)
            {
                return RedirectToAction("Index", "MissionSkills", new { Area = "Admin", pg = Convert.ToInt32(TempData["pg"]) - 1 });
            }
            if (TempData["msg"] != null)
                ViewBag.success = TempData["msg"];
            ViewBag.pager = pager;
            return View(MissionSkillData);
        }


        public IActionResult AddMissionSkill()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMissionSkill(Skill skill)
        {
            if (ModelState.IsValid)
            {
                _adminMissionSkillsRepository.InsertSkill(skill);
                _commonRepository.Save();
                TempData["msg"] = "Record Inserted Successfully!";
                return RedirectToAction("Index", "MissionSkills", new { Area = "Admin", pg = TempData["pg"] });
            }
            return View(skill);
        }


        public IActionResult EditMissionSkill(long id)
        {
            var skill = _adminMissionSkillsRepository.GetSkillById(id);
            if (skill != null)
            {
                return View(skill);
            }
            return RedirectToAction("Index", "MissionSkills", new { Area = "Admin", pg = TempData["pg"] });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMissionSkill(long id, Skill skill)
        {
            var newSkill = _adminMissionSkillsRepository.GetSkillById(id);
            if (ModelState.IsValid)
            {
                newSkill.UpdatedAt = DateTime.Now;
                newSkill.SkillName = skill.SkillName;
                newSkill.Status = skill.Status;
                _adminMissionSkillsRepository.UpdateSkill(newSkill);
                _commonRepository.Save();
                TempData["msg"] = "Record Edited Successfully!";
                return RedirectToAction("Index", "MissionSkills", new { Area = "Admin", pg = TempData["pg"] });
            }
            return View(skill);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteMissionSkill(long id)
        {
            var skill = _adminMissionSkillsRepository.GetSkillById(id);
            if (skill != null)
            {
                _adminMissionSkillsRepository.DeleteSkill(skill);
            }
            _commonRepository.Save();
            TempData["msg"] = "Record Deleted Successfully!";
            return RedirectToAction("Index", "MissionSkills", new { Area = "Admin", pg = TempData["pg"] });
        }
    }
}
