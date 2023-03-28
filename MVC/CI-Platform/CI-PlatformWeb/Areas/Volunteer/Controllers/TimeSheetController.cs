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
            List<Timesheet> timesheet = _timeSheetRepository.GetTimeSheetDataByUserId(Convert.ToInt64(uid));
            ViewBag.Missions = _commonRepository.GetMissionByUserApply(Convert.ToInt32(uid));
            return View(timesheet);
        }

        [HttpPost]
        public IActionResult AddVolunteering(long mission, string dateVol, int volHour, int volMin, int volAction, string volMessage)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            Timesheet timesheet = new Timesheet();
            timesheet.UserId = Convert.ToInt64(uid);
            timesheet.MissionId = mission;
            if (volAction == 0)
            {
                TimeSpan time = new TimeSpan(volHour, volMin, 0);
                timesheet.Time = time;
                timesheet.Action = null;
            }
            else
            {
                timesheet.Time = null;
                timesheet.Action = volAction;
            }
            timesheet.DateVolunteered = Convert.ToDateTime(dateVol);
            timesheet.Notes = volMessage;
            timesheet.Status = "SUBMIT_FOR_APPROVAL";

            _timeSheetRepository.InsertTimesheet(timesheet);
            _commonRepository.Save();
            return RedirectToAction("Index", "TimeSheet", new { Area = "Volunteer" });
        }
    }
}
