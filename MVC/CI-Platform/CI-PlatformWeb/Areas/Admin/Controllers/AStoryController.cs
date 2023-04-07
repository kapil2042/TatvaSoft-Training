using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers
{
    public class AStoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
