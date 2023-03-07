using CI_Platform.Models;
using CI_Platform.Models.ViewModels;
using CI_Platform.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeRepository _homeRepository;

        public HomeController(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }

        public IActionResult Index(string? search, string? country, string id, int pg = 1)
        {
            if (TempData["Logout"] != null)
                ViewBag.success = TempData["Logout"];
            VMMissions model = new VMMissions();
            model.countries = _homeRepository.GetCountries();
            model.cities = _homeRepository.GetCities();
            model.skills = _homeRepository.GetSkills();
            model.themes = _homeRepository.GetMissionThemes();
            model.goal = _homeRepository.GetGoalMissions();
            model.timesheet = _homeRepository.GetTimeSheet();
            model.missionSkills = _homeRepository.GetMissionSkills();

            List<Mission> m = _homeRepository.GetMissions();

            if (id != null)
            {
                switch (id)
                {
                    case "AZ":
                        m = _homeRepository.GetMissionsAtoZ();
                        break;
                    case "ZA":
                        m = _homeRepository.GetMissionsZtoA();
                        break;
                    case "NEW":
                        m = _homeRepository.GetMissionsNew();
                        break;
                    case "OLD":
                        m = _homeRepository.GetMissionsOld();
                        break;
                    default:
                        m = _homeRepository.GetMissions();
                        break;
                }
            }

            const int pageSize = 6;
            if (pg < 1)
            {
                pg = 1;
            }
            int recsCount = m.Count();
            var pager = new VMPager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = m.Skip(recSkip).Take(pager.PageSize).ToList();
            model.mission = data;
            this.ViewBag.pager = pager;
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public IActionResult Mission_volunteering()
        {
            ViewData["country"] = _homeRepository.GetCountries();
            ViewData["city"] = _homeRepository.GetCities();
            ViewData["skill"] = _homeRepository.GetSkills();
            ViewData["theme"] = _homeRepository.GetMissionThemes();
            ViewData["mission"] = _homeRepository.GetMissions();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}