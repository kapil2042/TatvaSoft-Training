using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.User.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
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
    }
}
