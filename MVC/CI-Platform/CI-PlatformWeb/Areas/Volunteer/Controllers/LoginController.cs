using CI_Platform.Data;
using CI_Platform.Models;
using Microsoft.AspNetCore.Mvc;


namespace CI_PlatformWeb.Areas.Volunteer.Controllers
{
    public class LoginController : Controller
    {
        private readonly CiPlatformContext _db;

        public LoginController(CiPlatformContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(User user)
        {
            return View();
        }
        public IActionResult ForgotPass()
        {
            return View();
        }
        public IActionResult ResetPass()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(User user)
        {
            user.CityId = 1;
            user.CountryId = 1;
            _db.Users.Add(user);
            _db.SaveChanges();
            return View();
        }
    }
}
