using CI_Platform.Models;
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

        public IActionResult Index()
        {
            return View();
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
