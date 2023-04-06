using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
