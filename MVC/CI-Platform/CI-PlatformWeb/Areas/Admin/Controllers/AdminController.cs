using CI_Platform.Models;
using CI_Platform.Models.ViewModels;
using CI_Platform.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CI_PlatformWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ICommonRepository _commonRepository;

        public AdminController(IAdminRepository adminRepository, ICommonRepository commonRepository)
        {
            _adminRepository = adminRepository;
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
            int recsCount = _adminRepository.GetTotalAdminsRecord(id);
            var pager = new VMPager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var adminData = _adminRepository.GetAdmins(id, recSkip, pager.PageSize);
            if (adminData.Count() == 0 && pg > 1)
            {
                return RedirectToAction("Index", "Admin", new { Area = "Admin", id = id, pg = Convert.ToInt32(TempData["pg"]) - 1 });
            }
            if (TempData["msg"] != null)
                ViewBag.success = TempData["msg"];
            ViewBag.pager = pager;
            ViewBag.query = id;
            return View(adminData);
        }

        public IActionResult ChangeAdminStatus(long id)
        {
            var admin = _adminRepository.GetAdminById(id);
            admin.UpdatedAt = DateTime.Now;
            if (admin.DeletedAt == null)
            {
                admin.DeletedAt = DateTime.Now;
                TempData["msg"] = "Admin Activate!";
            }
            else
            {
                admin.DeletedAt = null;
                TempData["msg"] = "Admin Deactivate!";
            }
            _commonRepository.UpdateAdmin(admin);
            _commonRepository.Save();
            return RedirectToAction("Index", "Admin", new { Area = "Admin", pg = TempData["pg"] });
        }

        public IActionResult AddNewAdmin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewAdmin(CI_Platform.Models.Admin admin, string cnfPass)
        {
            if (admin.Password == cnfPass && admin.Password != null)
            {
                if (ModelState.IsValid)
                {
                    admin.Password = _commonRepository.Encode(admin.Password);
                    _adminRepository.InsertAdmin(admin);
                    _commonRepository.Save();
                    return RedirectToAction("Index", "Admin", new { Area = "Admin", pg = TempData["pg"] });
                }
            }
            else
            {
                ViewBag.passerr = "Password and Confirm Password dose not match";
            }
            return View(admin);
        }

        public IActionResult UpdateProfile()
        {
            if (TempData["msg"] != null)
                ViewBag.success = TempData["msg"];
            if (TempData["emsg"] != null)
                ViewBag.error = TempData["emsg"];
            var identity = User.Identity as ClaimsIdentity;
            var email = identity?.FindFirst(ClaimTypes.Email)?.Value;
            return View(_commonRepository.getAdminByEmail(email));
        }

        [HttpPost]
        public IActionResult UpdateProfile(CI_Platform.Models.Admin admin)
        {
            ModelState.Remove("Password");
            if (ModelState.IsValid)
            {
                var adminData = _commonRepository.getAdminById(admin.AdminId);
                if (adminData != null)
                {
                    adminData.UpdatedAt = DateTime.Now;
                    adminData.FisrtName = admin.FisrtName;
                    adminData.LastName = admin.LastName;
                    _commonRepository.UpdateAdmin(adminData);
                    _commonRepository.Save();
                    //admin = adminData;
                    ViewBag.success = "Profile Updated Successfully";
                }
            }
            return View(admin);
        }

        [HttpPost]
        public IActionResult ChangePassword(long adminId, string oldPass, string newPass)
        {
            var adminData = _commonRepository.getAdminById(adminId);
            if (adminData != null)
            {
                var tempPass = _commonRepository.Decode(adminData.Password);
                if (tempPass == oldPass)
                {
                    adminData.Password = _commonRepository.Encode(newPass);
                    _commonRepository.UpdateAdmin(adminData);
                    _commonRepository.Save();
                    TempData["msg"] = "Password Changed Successfully!";
                }
                else
                {
                    TempData["emsg"] = "Old Password Not Matched!!";
                }
            }
            else
            {
                TempData["emsg"] = "Somthing went Wrong, Please try again later!!";
            }
            return RedirectToAction("UpdateProfile", "Admin", new { Area = "Admin" });
        }
    }
}
