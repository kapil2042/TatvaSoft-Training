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

        public IActionResult Index(string? search, string id, int pg = 1)
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
            model.missionRatings = _homeRepository.GetMissionsRating();

            List<Mission> m = _homeRepository.GetMissions();

            //if (search == null || search == "")
            //{
            //    search = (string?)TempData["search"];
            //}
            //TempData["search"] = search;
            
            if(search != "" && search != null)
            {
                m = _homeRepository.GetMissionsBySearch(search);
                if (m.Count() > 0)
                {
                    model.mission = m;
                }
                else
                {
                    m = _homeRepository.GetMissions();
                    ViewBag.nomission = 1;
                }
            }
            switch (id)
            {
                case "AZ":
                    m = m.OrderBy(x => x.Title).ToList();
                    break;
                case "ZA":
                    m = m.OrderByDescending(x => x.Title).ToList();
                    break;
                case "NEW":
                    m = m.OrderBy(x => x.StartDate).ToList();
                    break;
                case "OLD":
                    m = m.OrderByDescending(x => x.StartDate).ToList();
                    break;
            }

            const int pageSize = 6;
            if (pg < 1)
            {
                pg = 1;
            }
            int recsCount = m.Count();
            ViewBag.count = recsCount;
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
        public IActionResult Mission_volunteering(int id)
        {
            VMMissions model = new VMMissions();
            model.countries = _homeRepository.GetCountries();
            model.cities = _homeRepository.GetCities();
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
    }
}