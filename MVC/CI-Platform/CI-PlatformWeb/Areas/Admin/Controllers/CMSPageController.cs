using CI_Platform.Models.ViewModels;
using CI_Platform.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers
{
    public class CMSPageController : Controller
    {
        private readonly ICMSPageRepository _cmsPageRepository;

        public CMSPageController(ICMSPageRepository cmsPageRepository)
        {
            _cmsPageRepository = cmsPageRepository;
        }

        public IActionResult Index(int pg)
        {
            if (pg < 1)
            {
                pg = 1;
            }
            const int pageSize = 10;
            int recsCount = _cmsPageRepository.GetTotalCmsPageRecord();
            var pager = new VMPager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var CMSData = _cmsPageRepository.GetCmsPages(recSkip, pager.PageSize);
            ViewBag.pager = pager;
            return View(CMSData);
        }

        public IActionResult AddCmsPage()
        {
            return View();
        }
    }
}
