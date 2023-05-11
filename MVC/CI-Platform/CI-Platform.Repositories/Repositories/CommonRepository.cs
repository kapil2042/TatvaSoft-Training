using CI_Platform.Data;
using CI_Platform.Models;
using CI_Platform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repositories.Repositories
{
    public class CommonRepository : ICommonRepository
    {
        private readonly CiPlatformContext _db;

        public CommonRepository(CiPlatformContext db)
        {
            _db = db;
        }

        public Admin getAdminByEmail(string email)
        {
            return _db.Admins.Where(x => x.Email == email).FirstOrDefault();
        }

        public Admin getAdminById(long adminId)
        {
            return _db.Admins.Where(x => x.AdminId == adminId).FirstOrDefault();
        }

        public List<User> GetAllUsers()
        {
            return _db.Users.Where(x => x.Status == 1).ToList();
        }

        public User GetUserById(long id)
        {
            return _db.Users.Where(x => x.UserId == id && x.Status == 1).Include(x => x.UserSkills).ThenInclude(x => x.Skill).FirstOrDefault();
        }

        public List<Country> GetCountries()
        {
            return _db.Countries.ToList();
        }

        public List<Country> GetCountriesByNotDeleted()
        {
            return _db.Countries.Where(x => x.DeletedAt == null).ToList();
        }

        public List<City> GetCities()
        {
            return _db.Cities.ToList();
        }

        public List<City> GetCitiesBycountry(int country)
        {
            return _db.Cities.Where(x => x.CountryId == country).ToList();
        }

        public List<Skill> GetSkills()
        {
            return _db.Skills.Where(x => x.Status == 1).ToList();
        }

        public List<MissionTheme> GetMissionThemes()
        {
            return _db.MissionThemes.Where(x => x.Status == 1).ToList();
        }

        public void UpdateUser(User user)
        {
            _db.Users.Update(user);
        }

        public void UpdateAdmin(Admin admin)
        {
            _db.Admins.Update(admin);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void SendMails(string sub, string body, string[] mailids)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("lrs.aau.in@gmail.com");
                foreach (var mailid in mailids)
                {
                    message.To.Add(mailid);
                }
                message.Subject = sub;
                message.IsBodyHtml = true;
                message.Body = body;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("lrs.aau.in@gmail.com", "nlugxyghjtuxmmqj");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
        }

        public long[] GetMissionsIdBySkillName(string[] skill)
        {
            return _db.MissionSkills.Include(x => x.Skill).Where(x => skill.Contains(x.Skill.SkillName)).Select(x => x.MissionId).ToArray();
        }

        public long GetUserIdByEmail(string email)
        {
            return _db.Users.Where(x => x.Email.Equals(email)).Select(x => x.UserId).FirstOrDefault();
        }

        public List<Mission> GetMissionByUserApply(int id)
        {
            return _db.Missions.Where(x => (_db.MissionApplicatoins.Where(x => x.UserId == id).Select(x => x.MissionId).ToList()).Contains(x.MissionId)).ToList();
        }


        public string Encode(string text)
        {
            try
            {
                byte[] encData_byte = new byte[text.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(text);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Encode" + ex.Message);
            }
        }

        public string Decode(string encoded_text)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encoded_text);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public List<CmsPage> getAllPrivacy()
        {
            return _db.CmsPages.Where(x => x.Status == 1).ToList();
        }

        public List<Banner> GetBanners()
        {
            return _db.Banners.OrderBy(x => x.SortOrder).ToList();
        }

        public bool matchUserAndMissionSkills(long userId, long missionId)
        {
            var userSkill = _db.UserSkills.Where(x => x.UserId == userId).Select(x => x.SkillId).ToArray();
            var missionSkill = _db.MissionSkills.Where(x => x.MissionId == missionId).Select(x => x.SkillId).ToArray();
            var checkArray = missionSkill.Except(userSkill);
            if (checkArray.Count() == 0)
            {
                return true;
            }
            return false;
        }

        public List<City> GetCitiesByNotDeleted()
        {
            return _db.Cities.Where(x => x.DeletedAt == null).ToList();
        }

        public List<MissionTheme> GetMissionThemesByNotDeleted()
        {
            return _db.MissionThemes.Where(x => x.DeletedAt == null).ToList();
        }

        public bool isCountryDeleted(long id)
        {
            return _db.Countries.Where(x => x.CountryId == id && x.DeletedAt == null).Any();
        }

        public bool isCityDeleted(long id)
        {
            return _db.Cities.Where(x => x.CityId == id && x.DeletedAt == null).Any();
        }

        public bool isThemeDeleted(long id)
        {
            return _db.MissionThemes.Where(x => x.MissionThemeId == id && x.DeletedAt == null).Any();
        }

        public bool isUniqueCountry(string countryName)
        {
            return !_db.Countries.Where(x => x.Name.ToLower() == countryName.ToLower()).Any();
        }

        public bool isUniqueCity(string cityName, long countryId)
        {
            return !_db.Cities.Where(x => x.Name.ToLower() == cityName.ToLower() && x.CountryId == countryId).Any();
        }

        public bool isUniqueMissionTheme(string themeName)
        {
            return !_db.MissionThemes.Where(x => x.Title.ToLower() == themeName.ToLower()).Any();
        }

        public bool isUniqueSkill(string skillName)
        {
            return !_db.Skills.Where(x => x.SkillName.ToLower() == skillName.ToLower()).Any();
        }

        public bool isUniqueCountryEdit(long id, string countryName)
        {
            return !_db.Countries.Where(x => x.Name.ToLower() == countryName.ToLower() && x.CountryId != id).Any();
        }

        public bool isUniqueCityEdit(long id, string cityName, long countryId)
        {
            return !_db.Cities.Where(x => x.Name.ToLower() == cityName.ToLower() && x.CountryId == countryId && x.CityId != id).Any();
        }

        public bool isUniqueMissionThemeEdit(long id, string themeName)
        {
            return !_db.MissionThemes.Where(x => x.Title.ToLower() == themeName.ToLower() && x.MissionThemeId != id).Any();
        }

        public bool isUniqueSkillEdit(long id, string skillName)
        {
            return !_db.Skills.Where(x => x.SkillName.ToLower() == skillName.ToLower() && x.SkillId != id).Any();
        }





        // For Notifications

        public List<UserNotification> GetOlderNotifications(int userid)
        {
            return _db.UserNotification.Include(x => x.Notification).Include(x => x.User).Where(x => x.UserId == userid && x.DeletedAt == null && x.CreatedAt.AddDays(1) < DateTime.Now).OrderByDescending(x => x.CreatedAt).ToList();
        }


        public List<UserNotification> GetNewerNotifications(int userid)
        {
            return _db.UserNotification.Include(x => x.Notification).Include(x => x.User).Where(x => x.UserId == userid && x.DeletedAt == null && x.CreatedAt.AddDays(1) >= DateTime.Now).OrderByDescending(x => x.CreatedAt).ToList();
        }

        public void UpdateNotificationStatusById(long usernotificationid)
        {
            var record = _db.UserNotification.FirstOrDefault(x => x.UserNotificationId == usernotificationid);
            if (record != null)
            {
                record.Isread = 1;
                _db.UserNotification.Update(record);
            }
        }

        public NotificationSettings GetNotificationSettingsById(int userid)
        {
            return _db.NotificationSettings.FirstOrDefault(x => x.UserId == userid);
        }

        public void UpdateNotificationSettingsByUser(NotificationSettings notificationSettings)
        {
            _db.NotificationSettings.Update(notificationSettings);
        }

        public void DoAllSettingInactive(NotificationSettings notificationSettings)
        {
            notificationSettings.RecommendMission = 0;
            notificationSettings.RecommendStory = 0;
            notificationSettings.VolunteerGoal = 0;
            notificationSettings.VolunteerHour = 0;
            notificationSettings.CommentApprovation = 0;
            notificationSettings.MissionApplicationApprovation = 0;
            notificationSettings.StoryApprovation = 0;
            notificationSettings.NewMessage = 0;
            notificationSettings.News = 0;
            notificationSettings.FromEmail = 0;
        }

        public void DeleteNotificationsByUser(int userid)
        {
            var notificationList = _db.UserNotification.Where(x => x.UserId == userid).ToList();
            foreach (var notification in notificationList)
            {
                notification.DeletedAt = DateTime.Now;
                _db.UserNotification.Update(notification);
            }
        }

        //public void AddNotificationSettingsByUser(int userid)
        //{
        //    var record = _db.NotificationSettings.FirstOrDefault(x => x.UserId == userid);
        //    if (record == null)
        //    {
        //        NotificationSettings notificationSettings = new NotificationSettings();
        //        notificationSettings.UserId = userid;
        //        notificationSettings.RecommendMission = 0;
        //        notificationSettings.RecommendStory = 0;
        //        notificationSettings.VolunteerGoal = 0;
        //        notificationSettings.VolunteerHour = 0;
        //        notificationSettings.CommentApprovation = 0;
        //        notificationSettings.MissionApplicationApprovation = 0;
        //        notificationSettings.StoryApprovation = 0;
        //        notificationSettings.NewMessage = 0;
        //        notificationSettings.News = 0;
        //        notificationSettings.FromEmail = 0;
        //        _db.NotificationSettings.Add(notificationSettings);
        //    }
        //}

        public string getMissionTitleById(int missionId)
        {
            return _db.Missions.Where(x => x.MissionId == missionId).Select(x => x.Title).FirstOrDefault().ToString();
        }
        
        public string getStoryTitleById(int storyId)
        {
            return _db.Stories.Where(x => x.StoryId == storyId).Select(x => x.Title).FirstOrDefault().ToString();
        }

        public void InserNotification(Notification notification)
        {
            _db.Notification.Add(notification);
        }

        public NotificationSettings GetNotificationSettingsByUser(int userid)
        {
            return _db.NotificationSettings.FirstOrDefault(x => x.UserId == userid);
        }

        public int getTotalNotificationByUser(int userId)
        {
            return _db.UserNotification.Count(User => User.UserId == userId && User.Isread == 0 && User.DeletedAt == null);
        }
    }
}