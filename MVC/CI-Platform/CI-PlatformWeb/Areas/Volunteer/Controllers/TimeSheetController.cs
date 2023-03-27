using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers
{
    public class TimeSheetController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
