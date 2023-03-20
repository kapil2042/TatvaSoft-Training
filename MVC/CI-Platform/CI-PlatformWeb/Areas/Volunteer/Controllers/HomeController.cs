using CI_Platform.Models;
using CI_Platform.Models.ViewModels;
using CI_Platform.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeRepository _homeRepository;

        public HomeController(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }

        public IActionResult Index()
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            if (TempData["Logout"] != null)
                ViewBag.success = TempData["Logout"];
            VMMissions model = new VMMissions();
            model.users = _homeRepository.GetAllUsers();
            model.mission = _homeRepository.GetMissions();
            model.countries = _homeRepository.GetCountries();
            model.cities = _homeRepository.GetCities();
            model.skills = _homeRepository.GetSkills();
            model.themes = _homeRepository.GetMissionThemes();
            model.goal = _homeRepository.GetGoalMissions();
            model.timesheet = _homeRepository.GetTimeSheet();
            model.missionSkills = _homeRepository.GetMissionSkills();
            model.missionRatings = _homeRepository.GetMissionsRating();
            model.missionMedia = _homeRepository.GetMissionMedia();
            model.favoriteMission = _homeRepository.GetFavoriteMissionsByUserId(Convert.ToInt32(uid));
            model.missionApplicatoin = _homeRepository.GetMissionApplicatoinsByUserId(Convert.ToInt32(uid));
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult filterMissions(int id, string search, string country, string cities, string theme, string skill, int pg = 1)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            VMMissions model = new VMMissions();
            model.users = _homeRepository.GetAllUsers();
            model.countries = _homeRepository.GetCountries();
            model.cities = _homeRepository.GetCities();
            model.skills = _homeRepository.GetSkills();
            model.themes = _homeRepository.GetMissionThemes();
            model.goal = _homeRepository.GetGoalMissions();
            model.timesheet = _homeRepository.GetTimeSheet();
            model.missionSkills = _homeRepository.GetMissionSkills();
            model.missionRatings = _homeRepository.GetMissionsRating();
            model.missionMedia = _homeRepository.GetMissionMedia();
            model.favoriteMission = _homeRepository.GetFavoriteMissionsByUserId(Convert.ToInt32(uid));
            model.missionApplicatoin = _homeRepository.GetMissionApplicatoinsByUserId(Convert.ToInt32(uid));

            List<Mission> m = _homeRepository.GetMissions();

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
                m = m.Where(x => (_homeRepository.GetMissionsIdBySkillName(skilllist)).Contains(x.MissionId)).ToList();
            }

            if (!string.IsNullOrEmpty(search))
            {
                m = m.Where(x => x.Title.ToLower().Contains(search)).ToList();
            }
            switch (id)
            {
                case 1:
                    m = m.OrderBy(x => x.Title).ToList();
                    break;
                case 2:
                    m = m.OrderByDescending(x => x.Title).ToList();
                    break;
                case 3:
                    m = m.OrderBy(x => x.StartDate).ToList();
                    break;
                case 4:
                    m = m.OrderByDescending(x => x.StartDate).ToList();
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
            model.mission = data;
            ViewBag.pager = pager;
            return PartialView("partialFilterMissions", model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public IActionResult Mission_volunteering(int id)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            VMMissionVol model = new VMMissionVol();
            model.users = _homeRepository.GetAllUsers();
            model.countries = _homeRepository.GetCountries();
            model.cities = _homeRepository.GetCities();
            model.skills = _homeRepository.GetSkills();
            model.themes = _homeRepository.GetMissionThemes();
            model.mission = _homeRepository.GetMissionsById(id);
            model.goalMissions = _homeRepository.GetGoalMissionByMissionId(id);
            model.timesheet = _homeRepository.GetTimesheetByMissionId(id);
            model.missionMedia = _homeRepository.GetMissionMediaByMissionId(id);
            model.missionSkills = _homeRepository.GetMissionSkillsByMissionId(id);
            model.missionRating = _homeRepository.GetMissionRatingByUserIdAndMissionId(Convert.ToInt32(uid), id);
            model.sum = _homeRepository.GetSumOfMissionRatingByMissionId(id);
            model.total = _homeRepository.GetTotalMissionRatingByMissionId(id);
            model.favoriteMission = _homeRepository.GetFavoriteMissionsByUserIdAndMissionId(Convert.ToInt32(uid), id);
            model.missionApplicatoin = _homeRepository.GetMissionApplicatoinByUserIdAndMissionId(Convert.ToInt32(uid), id);
            model.volunteerDetails = _homeRepository.GetMissionApplicatoinsByMissionId(id);
            model.missionDocuments = _homeRepository.GetFavoriteMissionDocumentsByMissionId(id);
            model.comments = _homeRepository.GetCommentsByMissionId(id);

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public JsonResult GetCityByCountry(int country)
        {
            var results = _homeRepository.GetCitiesBycountry(country);
            return Json(results);
        }

        [Authorize]
        [HttpPost]
        public void Favourite_Mission(int missoinid)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            var data = _homeRepository.GetFavoriteMissionsByUserIdAndMissionId(Convert.ToInt32(uid), missoinid);
            if (data != null)
            {
                _homeRepository.UnlikeMission(data);
            }
            else
            {
                FavoriteMission favoriteMission = new FavoriteMission();
                favoriteMission.MissionId = missoinid;
                favoriteMission.UserId = Convert.ToInt32(uid);
                _homeRepository.LikeMission(favoriteMission);
            }
            _homeRepository.Save();
        }

        [Authorize]
        [HttpPost]
        public void Rating_Mission(int missoinid, int rat)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            var data = _homeRepository.GetMissionRatingByUserIdAndMissionId(Convert.ToInt32(uid), missoinid);
            if (data != null)
            {
                data.UpdatedAt = DateTime.Now;
                data.Rating = rat;
                _homeRepository.UpdateRating(data);
            }
            else
            {
                MissionRating missionRating = new MissionRating();
                missionRating.MissionId = missoinid;
                missionRating.UserId = Convert.ToInt32(uid);
                missionRating.Rating = rat;
                _homeRepository.Rating(missionRating);
            }
            _homeRepository.Save();
        }


        [Authorize]
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
            _homeRepository.PostComment(comment);
            _homeRepository.Save();
        }

        [Authorize]
        [HttpPost]
        public void Recommended_Mission(int missoinid, string[] mailids)
        {
            var link = Url.Action("ResetPass", "Login", new { Area = "Volunteer", id = missoinid });
            var mailBody = "<h1>Mission For You:</h1><br> <a href='https://localhost:44304" + link + "'> <b style='color:green;'>Click Here to See Mission Details</b>  </a>";

            _homeRepository.SendMails(mailBody,mailids);
        }
    }
}