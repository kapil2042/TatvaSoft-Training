using CI_Platform.Models;
using CI_Platform.Models.ViewModels;
using CI_Platform.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers
{
    [Area("Volunteer")]
    public class MissionController : Controller
    {
        private readonly ICommonRepository _commonRepository;
        private readonly IMissionRepository _missionRepository;

        public MissionController(ICommonRepository commonRepository, IMissionRepository missionRepository)
        {
            _commonRepository = commonRepository;
            _missionRepository = missionRepository;
        }

        public IActionResult Index(string? ReturnUrl)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            if (TempData["Logout"] != null)
                ViewBag.success = TempData["Logout"];
            VMMissions model = new()
            {
                users = _commonRepository.GetAllUsers(),
                mission = _missionRepository.GetMissions(),
                countries = _commonRepository.GetCountries(),
                cities = _commonRepository.GetCities(),
                skills = _commonRepository.GetSkills(),
                themes = _commonRepository.GetMissionThemes(),
                goal = _missionRepository.GetGoalMissions(),
                timesheet = _missionRepository.GetTimeSheet(),
                missionSkills = _missionRepository.GetMissionSkills(),
                missionRatings = _missionRepository.GetMissionsRating(),
                missionMedia = _missionRepository.GetMissionMedia(),
                favoriteMission = _missionRepository.GetFavoriteMissionsByUserId(Convert.ToInt32(uid)),
                missionApplicatoin = _missionRepository.GetMissionApplicatoinsByUserId(Convert.ToInt32(uid)),
            };
            if (ReturnUrl != null)
            {
                ViewBag.error = "Access Decline!";
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult FilterMissions(int id, string search, string country, string cities, string theme, string skill, int pg = 1)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            VMMissions model = new()
            {
                users = _commonRepository.GetAllUsers(),
                countries = _commonRepository.GetCountries(),
                cities = _commonRepository.GetCities(),
                skills = _commonRepository.GetSkills(),
                themes = _commonRepository.GetMissionThemes(),
                goal = _missionRepository.GetGoalMissions(),
                timesheet = _missionRepository.GetTimeSheet(),
                missionSkills = _missionRepository.GetMissionSkills(),
                missionRatings = _missionRepository.GetMissionsRating(),
                missionMedia = _missionRepository.GetMissionMedia(),
                favoriteMission = _missionRepository.GetFavoriteMissionsByUserId(Convert.ToInt32(uid)),
                missionApplicatoin = _missionRepository.GetMissionApplicatoinsByUserId(Convert.ToInt32(uid)),
                missionAppAll = _missionRepository.GetAllMissionApplicationSum(),
            };

            List<Mission> m = _missionRepository.GetMissions();

            if (country != null)
            {
                m = m.Where(x => x.Country.Name == country).ToList();
                if (m.Count() <= 0)
                {
                    ViewBag.nomission = 1;
                }
            }

            if (cities != null)
            {
                string[] citylist = cities.Split(',', StringSplitOptions.RemoveEmptyEntries);
                m = m.Where(x => (citylist.Contains(x.City.Name))).ToList();
            }

            if (theme != null)
            {
                string[] themelist = theme.Split(',', StringSplitOptions.RemoveEmptyEntries);
                m = m.Where(x => (themelist.Contains(x.Theme.Title))).ToList();
            }

            if (skill != null)
            {
                string[] skilllist = skill.Split(',', StringSplitOptions.RemoveEmptyEntries);
                m = m.Where(x => (_commonRepository.GetMissionsIdBySkillName(skilllist)).Contains(x.MissionId)).ToList();
            }

            if (!string.IsNullOrEmpty(search))
            {
                m = m.Where(x => x.Title.ToLower().Contains(search)).ToList();
            }
            long[] favoritemissions = _missionRepository.GetFavoriteMissioIdnByUser(uid).ToArray();
            var missionapplications = _missionRepository.GetAllMissionApplicationSum();
            switch (id)
            {
                case 1:
                    m = m.OrderBy(x => x.CreatedAt).ToList();
                    break;
                case 2:
                    m = m.OrderByDescending(x => x.CreatedAt).ToList();
                    break;
                case 3:
                    m = m.OrderBy(x => x.TotalSeat - (missionapplications.Where(y => y.MissionId == x.MissionId && y.ApprovalStatus == "APPROVE").Count())).ToList();
                    break;
                case 4:
                    m = m.OrderByDescending(x => x.TotalSeat - (missionapplications.Where(y => y.MissionId == x.MissionId && y.ApprovalStatus == "APPROVE").Count())).ToList();
                    break;
                case 5:
                    m = m.OrderByDescending(x => favoritemissions.Contains(x.MissionId)).ThenBy(x => x.MissionId).ToList();
                    break;
                case 6:
                    m = m.OrderByDescending(x => x.EndDate).ToList();
                    break;
                case 7:
                    m = m.OrderBy(x => x.Theme.Title).ThenBy(x => x.Country.Name).ThenBy(x => x.City.Name).ToList();
                    break;
            }

            const int pageSize = 6;
            if (pg < 1)
            {
                pg = 1;
            }
            int recsCount = m.Count();
            if (recsCount <= 0)
            {
                ViewBag.nomission = 1;
            }
            ViewBag.count = recsCount;
            var pager = new VMPager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = m.Skip(recSkip).Take(pager.PageSize).ToList();
            if (data.Count() == 0)
            {
                pager = new VMPager(recsCount, 1, pageSize);
                recSkip = 0;
                data = m.Skip(recSkip).Take(pager.PageSize).ToList();
            }
            model.mission = data;
            ViewBag.pager = pager;
            return PartialView("partialFilterMissions", model);
        }


        [HttpPost]
        [AllowAnonymous]
        public IActionResult RelatedMission(string country, string cities, string theme)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            VMMissions model = new()
            {
                users = _commonRepository.GetAllUsers(),
                countries = _commonRepository.GetCountries(),
                cities = _commonRepository.GetCities(),
                skills = _commonRepository.GetSkills(),
                themes = _commonRepository.GetMissionThemes(),
                goal = _missionRepository.GetGoalMissions(),
                timesheet = _missionRepository.GetTimeSheet(),
                missionSkills = _missionRepository.GetMissionSkills(),
                missionRatings = _missionRepository.GetMissionsRating(),
                missionMedia = _missionRepository.GetMissionMedia(),
                favoriteMission = _missionRepository.GetFavoriteMissionsByUserId(Convert.ToInt32(uid)),
                missionApplicatoin = _missionRepository.GetMissionApplicatoinsByUserId(Convert.ToInt32(uid)),
                missionAppAll = _missionRepository.GetAllMissionApplicationSum(),
                mission = _missionRepository.GetMissions().Where(x => x.Theme.Title == theme || x.Country.Name == country || x.City.Name == cities).OrderBy(x => x.Theme.Title).ThenBy(x => x.Country.Name).ThenBy(x => x.City.Name).ToList(),
            };
            return PartialView("partialFilterMissions", model);
        }


        [Authorize(Policy = "VolunteerOnly")]
        public IActionResult Mission_volunteering(int id)
        {
            if (TempData["alredyapplied"] != null)
            {
                ViewBag.error = TempData["alredyapplied"];
            }
            if (TempData["msg"] != null)
                ViewBag.success = TempData["msg"];
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            VMMissionVol model = new()
            {
                users = _commonRepository.GetAllUsers(),
                countries = _commonRepository.GetCountries(),
                cities = _commonRepository.GetCities(),
                skills = _commonRepository.GetSkills(),
                themes = _commonRepository.GetMissionThemes(),
                mission = _missionRepository.GetMissionsById(id),
                goalMissions = _missionRepository.GetGoalMissionByMissionId(id),
                timesheet = _missionRepository.GetTimesheetByMissionId(id),
                missionMedia = _missionRepository.GetMissionMediaByMissionId(id),
                missionSkills = _missionRepository.GetMissionSkillsByMissionId(id),
                missionRating = _missionRepository.GetMissionRatingByUserIdAndMissionId(Convert.ToInt32(uid), id),
                sum = _missionRepository.GetSumOfMissionRatingByMissionId(id),
                total = _missionRepository.GetTotalMissionRatingByMissionId(id),
                favoriteMission = _missionRepository.GetFavoriteMissionsByUserIdAndMissionId(Convert.ToInt32(uid), id),
                missionApplicatoin = _missionRepository.GetMissionApplicatoinByUserIdAndMissionId(Convert.ToInt32(uid), id),
                volunteerDetails = _missionRepository.GetMissionApplicatoinsByMissionId(id),
                missionDocuments = _missionRepository.GetFavoriteMissionDocumentsByMissionId(id),
                comments = _missionRepository.GetCommentsByMissionId(id),
            };
            return View(model);
        }


        [Authorize(Policy = "VolunteerOnly")]
        [HttpPost]
        public void Favourite_Mission(int missoinid)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            var data = _missionRepository.GetFavoriteMissionsByUserIdAndMissionId(Convert.ToInt32(uid), missoinid);
            if (data != null)
            {
                _missionRepository.UnlikeMission(data);
            }
            else
            {
                FavoriteMission favoriteMission = new FavoriteMission();
                favoriteMission.MissionId = missoinid;
                favoriteMission.UserId = Convert.ToInt32(uid);
                _missionRepository.LikeMission(favoriteMission);
            }
            _commonRepository.Save();
        }

        [Authorize(Policy = "VolunteerOnly")]
        [HttpPost]
        public void Rating_Mission(int missoinid, int rat)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            var data = _missionRepository.GetMissionRatingByUserIdAndMissionId(Convert.ToInt32(uid), missoinid);
            if (data != null)
            {
                data.UpdatedAt = DateTime.Now;
                data.Rating = rat;
                _missionRepository.UpdateRating(data);
            }
            else
            {
                MissionRating missionRating = new MissionRating();
                missionRating.MissionId = missoinid;
                missionRating.UserId = Convert.ToInt32(uid);
                missionRating.Rating = rat;
                _missionRepository.Rating(missionRating);
            }
            _commonRepository.Save();
        }


        [Authorize(Policy = "VolunteerOnly")]
        [HttpPost]
        public void Post_Comment(int missoinid, string commenttext)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            Comment comment = new Comment();
            comment.UserId = Convert.ToInt32(uid);
            comment.CommentText = commenttext;
            comment.MissionId = missoinid;
            comment.ApprovalStatus = "PENDING";
            _missionRepository.PostComment(comment);
            _commonRepository.Save();
        }

        [Authorize(Policy = "VolunteerOnly")]
        [HttpPost]
        public void Recommended_Mission(int missoinid, string[] mailids)
        {
            int cnt = 0;
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            var firstName = identity?.FindFirst(ClaimTypes.Name)?.Value;
            var lastName = identity?.FindFirst(ClaimTypes.Surname)?.Value;
            var link = Url.Action("Mission_volunteering", "Mission", new { Area = "Volunteer", id = missoinid });
            var mailBody = "<h1>Mission For You:</h1><br> <a href='https://localhost:44332" + link + "'> <b style='color:green;'>Click Here to See Mission Details</b>  </a>";
            Notification notification = new Notification();
            notification.NotificationText = firstName + " " + lastName + " - Recommends this mission - " + _commonRepository.getMissionTitleById(missoinid);
            notification.FromUserId = Convert.ToInt32(uid);
            notification.NotificationType = 0;
            notification.CreatedAt = DateTime.Now;

            foreach (var mail in mailids)
            {
                long toUserId = _commonRepository.GetUserIdByEmail(mail);
                if (_commonRepository.GetNotificationSettingsByUser((int)toUserId).RecommendMission == 1)
                {
                    UserNotification userNotification = new UserNotification();
                    userNotification.UserId = toUserId;
                    userNotification.Isread = 0;
                    userNotification.CreatedAt = DateTime.Now;
                    notification.UserNotifications.Add(userNotification);
                    cnt++;
                }
                MissionInvite invite = new MissionInvite();
                invite.MissionId = missoinid;
                invite.FromUserId = Convert.ToInt64(uid);
                invite.ToUserId = toUserId;
                _missionRepository.InserMissionInvitation(invite);
            }
            if (cnt != 0)
                _commonRepository.InserNotification(notification);
            _commonRepository.Save();
            _commonRepository.SendMails("Mission Recommended", mailBody, mailids);
        }


        [Authorize(Policy = "VolunteerOnly")]
        [HttpPost]
        public IActionResult GetVolunteer(int mid, int pg)
        {
            List<MissionApplicatoin> list = new List<MissionApplicatoin>();

            list = _missionRepository.GetMissionApplicatoinsByMissionId(mid);

            const int pageSize = 9;
            if (pg < 1)
            {
                pg = 1;
            }
            int recsCount = list.Count();

            ViewBag.volcount = recsCount;
            var pager = new VMPager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            list = list.Skip(recSkip).Take(pager.PageSize).ToList();
            ViewBag.pager = pager;
            return PartialView("volunteerPagination", list);
        }

        [Authorize(Policy = "VolunteerOnly")]
        public IActionResult ApplyMission(long missionId)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            var mission = _missionRepository.GetMissionsById(Convert.ToInt32(missionId));
            var totalapplication = _missionRepository.GetMissionApplicatoinsByMissionId(Convert.ToInt32(missionId)).Count();
            if (!_commonRepository.matchUserAndMissionSkills(Convert.ToInt64(uid), missionId))
            {
                TempData["alredyapplied"] = "Your Skills are not matched with the mission skills! Please change your skills and try again!";
            }
            else if (mission.MissionType.Equals("TIME") && mission.EndDate < DateTime.Now)
            {
                TempData["alredyapplied"] = "Mission was expired!";
            }
            else if (_missionRepository.GetMissionApplicatoinByUserIdAndMissionId(Convert.ToInt32(uid), Convert.ToInt32(missionId)) != null)
            {
                TempData["alredyapplied"] = "You Already Applied on This Mission!";
            }
            else if (mission.TotalSeat <= totalapplication)
            {
                TempData["alredyapplied"] = "No any seat left on This Mission!";
            }
            else
            {
                MissionApplicatoin ma = new MissionApplicatoin();
                ma.MissionId = missionId;
                ma.UserId = Convert.ToInt64(uid);
                ma.AppliedAt = DateTime.Now;
                ma.ApprovalStatus = "PENDING";
                _missionRepository.InserMissionApplication(ma);
                _commonRepository.Save();
                TempData["msg"] = "Your application has been sent successfully! Wait for it's Approval";
            }
            return RedirectToAction("Mission_volunteering", "Mission", new { Area = "Volunteer", id = missionId });
        }
    }
}
