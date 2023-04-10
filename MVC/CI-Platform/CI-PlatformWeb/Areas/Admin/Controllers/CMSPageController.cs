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
    public class CMSPageController : Controller
    {
        private readonly IAdminCMSPageRepository _cmsPageRepository;
        private readonly ICommonRepository _commonRepository;

        public CMSPageController(IAdminCMSPageRepository cmsPageRepository, ICommonRepository commonRepository)
        {
            _cmsPageRepository = cmsPageRepository;
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
            int recsCount = _cmsPageRepository.GetTotalCmsPageRecord();
            var pager = new VMPager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var CMSData = _cmsPageRepository.GetCmsPages(recSkip, pager.PageSize);
            if (CMSData.Count() == 0 && pg > 1)
            {
                return RedirectToAction("Index", "CMSPage", new { Area = "Admin", pg = Convert.ToInt32(TempData["pg"]) - 1 });
            }
            if (TempData["msg"] != null)
                ViewBag.success = TempData["msg"];
            ViewBag.pager = pager;
            return View(CMSData);
        }

        public IActionResult AddCmsPage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCmsPage(CmsPage cms)
        {
            if (ModelState.IsValid)
            {
                _cmsPageRepository.InsertCmsPage(cms);
                _commonRepository.Save();
                TempData["msg"] = "Record Inserted Successfully!";
                return RedirectToAction("Index", "CMSPage", new { Area = "Admin", pg = TempData["pg"] });
            }
            return View(cms);
        }

        public IActionResult EditCmsPage(long id)
        {
            var cms = _cmsPageRepository.GetCmsPageById(id);
            if (cms != null)
            {
                cms.Description = WebUtility.HtmlDecode(cms.Description);
                return View(cms);
            }
            return RedirectToAction("Index", "CMSPage", new { Area = "Admin", pg = TempData["pg"] });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCmsPage(long id, CmsPage cms)
        {
            var newCms = _cmsPageRepository.GetCmsPageById(id);
            if (ModelState.IsValid)
            {
                newCms.UpdatedAt = DateTime.Now;
                newCms.Title = cms.Title;
                newCms.Description = WebUtility.HtmlEncode(cms.Description);
                newCms.Slug = cms.Slug;
                newCms.Status = cms.Status;
                _cmsPageRepository.UpdateCmsPage(newCms);
                _commonRepository.Save();
                TempData["msg"] = "Record Edited Successfully!";
                return RedirectToAction("Index", "CMSPage", new { Area = "Admin", pg = TempData["pg"] });
            }
            return View(cms);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCmsPage(long id)
        {
            var cms = _cmsPageRepository.GetCmsPageById(id);
            if (cms != null)
            {
                _cmsPageRepository.DeleteCmsPage(cms);
            }
            _commonRepository.Save();
            TempData["msg"] = "Record Deleted Successfully!";
            return RedirectToAction("Index", "CMSPage", new { Area = "Admin", pg = TempData["pg"] });
        }
    }
}
