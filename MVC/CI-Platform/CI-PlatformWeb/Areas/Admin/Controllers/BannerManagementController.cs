using CI_Platform.Models;
using CI_Platform.Models.ViewModels;
using CI_Platform.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class BannerManagementController : Controller
    {
        private readonly IAdminBannerRepository _bannerRepository;
        private readonly ICommonRepository _commonRepository;

        public BannerManagementController(IAdminBannerRepository bannerRepository, ICommonRepository commonRepository)
        {
            _bannerRepository = bannerRepository;
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
            const int pageSize = 3;
            int recsCount = _bannerRepository.GetTotalBannerRecord(id);
            var pager = new VMPager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var BannerData = _bannerRepository.GetBanners(id, recSkip, pager.PageSize);
            if (BannerData.Count() == 0 && pg > 1)
            {
                return RedirectToAction("Index", "BannerManagement", new { Area = "Admin", id = id, pg = Convert.ToInt32(TempData["pg"]) - 1 });
            }
            if (TempData["msg"] != null)
                ViewBag.success = TempData["msg"];
            ViewBag.pager = pager;
            ViewBag.query = id;
            return View(BannerData);
        }

        public IActionResult AddBanner()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBanner(Banner banner, IFormFile imageFile)
        {
            if (imageFile != null)
            {
                ModelState.Remove("Image");
            }
            if (ModelState.IsValid)
            {
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/banners", ImageName);

                using (var stream = new FileStream(SavePath, FileMode.Create))
                {
                    banner.Image = ImageName;
                    imageFile.CopyTo(stream);
                }
                _bannerRepository.InsertBanner(banner);
                _commonRepository.Save();
                TempData["msg"] = "Record Inserted Successfully!";
                return RedirectToAction("Index", "BannerManagement", new { Area = "Admin", pg = TempData["pg"] });
            }
            return View(banner);
        }

        public IActionResult EditBanner(long id)
        {
            var banner = _bannerRepository.GetBannerById(id);
            if (banner != null)
            {
                return View(banner);
            }
            return RedirectToAction("Index", "BannerManagement", new { Area = "Admin", pg = TempData["pg"] });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditBanner(long id, Banner banner, IFormFile imageFile)
        {
            var newBanner = _bannerRepository.GetBannerById(id);
            ModelState.Remove("Image");
            ModelState.Remove("imageFile");
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/banners", newBanner.Image);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                    string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/banners", ImageName);
                    using (var stream = new FileStream(SavePath, FileMode.Create))
                    {
                        newBanner.Image = ImageName;
                        imageFile.CopyTo(stream);
                    }
                }
                newBanner.UpdatedAt = DateTime.Now;
                newBanner.Title = banner.Title;
                newBanner.Text = banner.Text;
                newBanner.SortOrder = banner.SortOrder;
                _bannerRepository.UpdateBanner(newBanner);
                _commonRepository.Save();
                TempData["msg"] = "Record Edited Successfully!";
                return RedirectToAction("Index", "BannerManagement", new { Area = "Admin", pg = TempData["pg"] });
            }
            return View(banner);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteBanner(long id)
        {
            var banner = _bannerRepository.GetBannerById(id);
            if (banner != null)
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/banners", banner.Image);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                _bannerRepository.DeleteBanner(banner);
            }
            _commonRepository.Save();
            TempData["msg"] = "Record Deleted Successfully!";
            return RedirectToAction("Index", "BannerManagement", new { Area = "Admin", pg = TempData["pg"] });
        }
    }
}
