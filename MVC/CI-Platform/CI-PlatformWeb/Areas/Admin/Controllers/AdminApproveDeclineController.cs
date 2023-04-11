using CI_Platform.Models.ViewModels;
using CI_Platform.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class AdminApproveDeclineController : Controller
    {
        private readonly IAdminApproveDeclineRepository _adminApproveDeclineRepository;
        private readonly ICommonRepository _commonRepository;

        public AdminApproveDeclineController(IAdminApproveDeclineRepository AdminApproveDeclineRepository, ICommonRepository commonRepository)
        {
            _adminApproveDeclineRepository = AdminApproveDeclineRepository;
            _commonRepository = commonRepository;
        }

        public IActionResult MissionApplication(string id, int pg)
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
            int recsCount = _adminApproveDeclineRepository.GetTotalMissionApplicationRecord(id);
            var pager = new VMPager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var MissionAppData = _adminApproveDeclineRepository.GetMissionApplications(id, recSkip, pager.PageSize);
            if (MissionAppData.Count() == 0 && pg > 1)
            {
                return RedirectToAction("MissionApplication", "AdminApproveDecline", new { Area = "Admin", id = id, pg = Convert.ToInt32(TempData["pg"]) - 1 });
            }
            if (TempData["msg"] != null)
                ViewBag.success = TempData["msg"];
            ViewBag.pager = pager;
            ViewBag.query = id;
            return View(MissionAppData);
        }

        public IActionResult ChangeMissionApplicationStatus(long id, bool action)
        {
            var application = _adminApproveDeclineRepository.GetMissionApplicationById(id);
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
            _adminApproveDeclineRepository.UpdateMissionApplicationStatus(application);
            _commonRepository.Save();
            return RedirectToAction("MissionApplication", "AdminApproveDecline", new { Area = "Admin", pg = TempData["pg"] });
        }

        public IActionResult Story(int pg)
        {
            //if (TempData["msg"] != null)
            //    ViewBag.success = TempData["msg"];
            //TempData["pg"] = pg;
            //if (pg < 1)
            //{
            //    pg = 1;
            //}
            //const int pageSize = 10;
            //int recsCount = _adminApproveDeclineRepository.GetTotalMissionApplicationRecord();
            //var pager = new VMPager(recsCount, pg, pageSize);
            //int recSkip = (pg - 1) * pageSize;
            //var MissionAppData = _adminApproveDeclineRepository.GetMissionApplications(recSkip, pager.PageSize);
            //if (MissionAppData.Count() == 0 && pg > 1)
            //{
            //    return RedirectToAction("MissionApplication", "AdminApproveDecline", new { Area = "Admin", pg = Convert.ToInt32(TempData["pg"]) - 1 });
            //}
            //if (TempData["msg"] != null)
            //    ViewBag.success = TempData["msg"];
            //ViewBag.pager = pager;
            return View();
        }
    }
}
