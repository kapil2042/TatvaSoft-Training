using CI_Platform.Models.ViewModels;
using CI_Platform.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class MissionApplicationController : Controller
    {
        private readonly IAdminMissionApplicationRepository _adminMissionApplicationRepository;
        private readonly ICommonRepository _commonRepository;

        public MissionApplicationController(IAdminMissionApplicationRepository missionApplicationRepository, ICommonRepository commonRepository)
        {
            _adminMissionApplicationRepository = missionApplicationRepository;
            _commonRepository = commonRepository;
        }

        public IActionResult Index(int pg)
        {
            if (TempData["msg"] != null)
                ViewBag.success = TempData["msg"];
            TempData["pg"] = pg;
            if (pg < 1)
            {
                pg = 1;
            }
            const int pageSize = 10;
            int recsCount = _adminMissionApplicationRepository.GetTotalMissionApplicationRecord();
            var pager = new VMPager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var MissionAppData = _adminMissionApplicationRepository.GetMissionApplications(recSkip, pager.PageSize);
            if (MissionAppData.Count() == 0 && pg > 1)
            {
                return RedirectToAction("Index", "MissionApplication", new { Area = "Admin", pg = Convert.ToInt32(TempData["pg"]) - 1 });
            }
            if (TempData["msg"] != null)
                ViewBag.success = TempData["msg"];
            ViewBag.pager = pager;
            return View(MissionAppData);
        }

        public IActionResult ChangeMissionApplicationStatus(long id, bool action)
        {
            var application = _adminMissionApplicationRepository.GetMissionApplicationById(id);
            application.UpdatedAt = DateTime.Now;
            if (action)
            {
                application.ApprovalStatus = "APPROVE";
                TempData["msg"] = "Mission Application Approved successfully!";
            }
            else
            {
                application.ApprovalStatus = "DECLINE";
                TempData["msg"] = "Mission Application Declined successfully!";
            }
            _adminMissionApplicationRepository.UpdateMissionApplicationStatus(application);
            _commonRepository.Save();
            return RedirectToAction("Index", "MissionApplication", new { Area = "Admin", pg = TempData["pg"] });
        }
    }
}
