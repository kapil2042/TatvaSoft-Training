using CI_Platform.Models.ViewModels;
using CI_Platform.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            ViewBag.Country = _commonRepository.GetCountries();
            ViewBag.Theme = _commonRepository.GetMissionThemes();
            ViewBag.Skills = _commonRepository.GetSkills();
            return View();
        }
    }
}
