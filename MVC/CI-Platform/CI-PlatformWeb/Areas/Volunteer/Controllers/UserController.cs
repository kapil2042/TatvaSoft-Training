using CI_Platform.Models;
using CI_Platform.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ICommonRepository _commonRepository;

        public UserController(ICommonRepository commonRepository)
        {
            _commonRepository = commonRepository;
        }

        public IActionResult UserProfile()
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            ViewBag.city = _commonRepository.GetCities();
            ViewBag.country = _commonRepository.GetCountries();
            return View(_commonRepository.GetUserById(Convert.ToInt64(uid)));
        }

        [HttpPost]
        public IActionResult UserProfile(User user, string[] userSkills)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            ViewBag.city = _commonRepository.GetCities();
            ViewBag.country = _commonRepository.GetCountries();
            return View(_commonRepository.GetUserById(Convert.ToInt64(uid)));
        }


        [HttpPost]
        public IActionResult ChangePassword(string oldPass, string newPass, string cnfPass)
        {
            return RedirectToAction("UserProfile", "User", new { Area = "Volunteer" });
        }
    }
}
