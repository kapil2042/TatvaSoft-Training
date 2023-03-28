using CI_Platform.Models;
using CI_Platform.Models.ViewModels;
using CI_Platform.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers
{
    [Authorize]
    public class TimeSheetController : Controller
    {
        private readonly ITimeSheetRepository _timeSheetRepository;
        private readonly ICommonRepository _commonRepository;

        public TimeSheetController(ITimeSheetRepository timeSheetRepository, ICommonRepository commonRepository)
        {
            _timeSheetRepository = timeSheetRepository;
            _commonRepository = commonRepository;
        }

        public IActionResult Index()
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            VMTimeSheet timeSheet = new VMTimeSheet();
            timeSheet.Timesheets = _timeSheetRepository.GetTimeSheetDataByUserId(Convert.ToInt64(uid));
            timeSheet.Missions = _commonRepository.GetMissionByUserApply(Convert.ToInt32(uid));
            return View(timeSheet);
        }

        [HttpPost]
        public IActionResult AddVolunteering(VMTimeSheet timeSheet)
        {
            return RedirectToAction("Index", "TimeSheet", new { Area = "Volunteer" });
        }
    }
}
