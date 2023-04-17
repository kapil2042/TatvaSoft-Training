using CI_Platform.Models;
using CI_Platform.Models.ViewModels;
using CI_Platform.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers
{
    [Area("Volunteer")]
    [Authorize(Policy = "VolunteerOnly")]
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
            if (TempData["msg"] != null)
            {
                ViewBag.success = TempData["msg"];
            }
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            List<Timesheet> timesheet = _timeSheetRepository.GetTimeSheetDataByUserId(Convert.ToInt64(uid));
            ViewBag.Missions = _timeSheetRepository.GetMissionByUserApplyAndAppApproved(Convert.ToInt64(uid));
            return View(timesheet);
        }

        [HttpPost]
        public IActionResult Volunteering(long tid, long mission, string dateVol, int volHour, int volMin, int volAction, string volMessage, string btn)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            Timesheet timesheet = new Timesheet();
            if (btn == "Update")
            {
                timesheet = _timeSheetRepository.GetTimeSheetDataById(tid);
            }
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

            if (btn == "Save")
            {
                _timeSheetRepository.InsertTimesheet(timesheet);
                TempData["msg"] = "Record Inserteded Successfully! You Can see your record after Admin Approval";
            }
            if (btn == "Update")
            {
                _timeSheetRepository.UpdateTimesheet(timesheet);
                TempData["msg"] = "Record Updated Successfully! You Can see your record after Admin Approval";
            }
            _commonRepository.Save();
            return RedirectToAction("Index", "TimeSheet", new { Area = "Volunteer" });
        }

        public IActionResult DeleteVolunteeringTimeSheet(long timeSheetId)
        {
            _timeSheetRepository.DeleteTimesheet(_timeSheetRepository.GetTimeSheetDataById(timeSheetId));
            _commonRepository.Save();
            TempData["msg"] = "Record Deleted Successfully!";
            return RedirectToAction("Index", "TimeSheet", new { Area = "Volunteer" });
        }

        [HttpPost]
        public JsonResult GetTimeSheetData(long timeSheetId)
        {
            return Json(_timeSheetRepository.GetTimeSheetDataById(timeSheetId));
        }
    }
}
