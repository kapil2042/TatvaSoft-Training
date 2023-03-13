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
            model.mission = _homeRepository.GetMissions();
            model.countries = _homeRepository.GetCountries();
            model.cities = _homeRepository.GetCities();
            model.skills = _homeRepository.GetSkills();
            model.themes = _homeRepository.GetMissionThemes();
            model.goal = _homeRepository.GetGoalMissions();
            model.timesheet = _homeRepository.GetTimeSheet();
            model.missionSkills = _homeRepository.GetMissionSkills();
            model.missionRatings = _homeRepository.GetMissionsRating();
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
            model.countries = _homeRepository.GetCountries();
            model.cities = _homeRepository.GetCities();
            model.skills = _homeRepository.GetSkills();
            model.themes = _homeRepository.GetMissionThemes();
            model.goal = _homeRepository.GetGoalMissions();
            model.timesheet = _homeRepository.GetTimeSheet();
            model.missionSkills = _homeRepository.GetMissionSkills();
            model.missionRatings = _homeRepository.GetMissionsRating();
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
                //string[] skilllist = skill.Split(',', StringSplitOptions.RemoveEmptyEntries);
                //m = m.Where(x => skilllist.Contains(x.mms).ToList();
            }

            if (search != "" && search != null)
            {
                m = _homeRepository.GetMissionsBySearch(search);
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
            VMMissions model = new VMMissions();
            model.countries = _homeRepository.GetCountries();
            model.skills = _homeRepository.GetSkills();
            model.themes = _homeRepository.GetMissionThemes();
            model.goal = _homeRepository.GetGoalMissions();
            model.timesheet = _homeRepository.GetTimeSheet();
            model.missionSkills = _homeRepository.GetMissionSkills();
            model.missionRatings = _homeRepository.GetMissionsRating();
            model.m = _homeRepository.GetMissionsById(id);
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
    }
}