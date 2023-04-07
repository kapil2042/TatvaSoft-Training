using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers
{
    public class BannerManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
