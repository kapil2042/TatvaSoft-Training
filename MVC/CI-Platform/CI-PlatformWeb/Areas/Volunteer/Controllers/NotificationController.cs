using CI_Platform.Models.ViewModels;
using CI_Platform.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers
{
    [Area("Volunteer")]
    [Authorize(Policy = "VolunteerOnly")]
    public class NotificationController : Controller
    {
        private readonly ICommonRepository _commonRepository;

        public NotificationController(ICommonRepository commonRepository)
        {
            _commonRepository = commonRepository;
        }

        [HttpPost]
        public IActionResult GetNotificationofUser()
        {
            var identity = User.Identity as ClaimsIdentity;
            var userid = Convert.ToInt32(identity?.FindFirst(ClaimTypes.Sid)?.Value);
            VMNotification vMNotification = new VMNotification();
            vMNotification.NotificationSettings = _commonRepository.GetNotificationSettingsById(userid);
            vMNotification.OldUserNotifications = _commonRepository.GetOlderNotifications(userid);
            vMNotification.NewUserNotifications = _commonRepository.GetNewerNotifications(userid);
            vMNotification.Users = _commonRepository.GetAllUsers();
            //List<UserNotification> usernotification = _commonRepository.GetAllNotification(userid);
            return PartialView("_Notification", vMNotification);
        }


        [HttpPost]
        public long MakeReadedNotification(long usernotificationid)
        {
            _commonRepository.UpdateNotificationStatusById(usernotificationid);
            _commonRepository.Save();
            return usernotificationid;
        }


        [HttpPost]
        public void SaveNotificationSettings(string[] settingsarray)
        {
            var identity = User.Identity as ClaimsIdentity;
            var userid = Convert.ToInt32(identity?.FindFirst(ClaimTypes.Sid)?.Value);
            var settingrecord = _commonRepository.GetNotificationSettingsById(userid);
            _commonRepository.DoAllSettingInactive(settingrecord);
            foreach (var setting in settingsarray)
            {
                switch (setting)
                {
                    case "recommendedmission":
                        settingrecord.RecommendMission = 1;
                        break;
                    case "recommendedstory":
                        settingrecord.RecommendStory = 1;
                        break;
                    case "volunteerhour":
                        settingrecord.VolunteerHour = 1;
                        break;
                    case "volunteergoal":
                        settingrecord.VolunteerGoal = 1;
                        break;
                    case "mycomment":
                        settingrecord.CommentApprovation = 1;
                        break;
                    case "mystory":
                        settingrecord.StoryApprovation = 1;
                        break;
                    case "newmission":
                        settingrecord.NewMisson = 1;
                        break;
                    case "newmessage":
                        settingrecord.NewMessage = 1;
                        break;
                    case "missionapplication":
                        settingrecord.MissionApplicationApprovation = 1;
                        break;
                    case "news":
                        settingrecord.News = 1;
                        break;
                    case "fromemail":
                        settingrecord.FromEmail = 1;
                        break;
                }
            }
            _commonRepository.UpdateNotificationSettingsByUser(settingrecord);
            _commonRepository.Save();
        }


        [HttpPost]
        public void ClearAllNotification(int userid)
        {
            _commonRepository.DeleteNotificationsByUser(userid);
            _commonRepository.Save();
        }

        [HttpPost]
        public int GetTotalNotifications()
        {
            var identity = User.Identity as ClaimsIdentity;
            var userid = Convert.ToInt32(identity?.FindFirst(ClaimTypes.Sid)?.Value);
            return _commonRepository.getTotalNotificationByUser(Convert.ToInt32(userid));
        }

        //public void AddUserNotificationSettingsRecord(int userid)
        //{
        //    _commonRepository.AddNotificationSettingsByUser(userid);
        //    _commonRepository.Save();
        //}
    }
}
