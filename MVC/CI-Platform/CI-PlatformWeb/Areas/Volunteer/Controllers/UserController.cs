using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers
{
    public class UserController : Controller
    {
        public IActionResult UserProfile()
        {
            return View();
        }
    }
}
