using CI_Platform.Models;
using CI_Platform.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers
{
    public class TimeSheetController : Controller
    {
        private readonly ITimeSheetRepository _timeSheetRepository;

        public TimeSheetController(ITimeSheetRepository timeSheetRepository)
        {
            _timeSheetRepository = timeSheetRepository;
        }

        public IActionResult Index()
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            List<Timesheet> timesheets = _timeSheetRepository.GetTimeSheetDataByUserId(Convert.ToInt64(uid));
            return View(timesheets);
        }
    }
}
