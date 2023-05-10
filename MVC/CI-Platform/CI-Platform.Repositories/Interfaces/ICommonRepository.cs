using CI_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repositories.Interfaces
{
    public interface ICommonRepository
    {
        Admin getAdminByEmail(string email);

        Admin getAdminById(long adminId);

        List<User> GetAllUsers();

        User GetUserById(long id);

        List<Country> GetCountries();

        List<Country> GetCountriesByNotDeleted();

        List<City> GetCities();

        List<City> GetCitiesByNotDeleted();

        List<City> GetCitiesBycountry(int country);

        List<MissionTheme> GetMissionThemes();

        List<MissionTheme> GetMissionThemesByNotDeleted();

        List<Skill> GetSkills();

        void UpdateUser(User user);

        void UpdateAdmin(Admin admin);

        void Save();

        void SendMails(string sub, string body, string[] mailids);

        long[] GetMissionsIdBySkillName(string[] skill);

        long GetUserIdByEmail(string email);

        List<Mission> GetMissionByUserApply(int id);

        string Encode(string text);

        string Decode(string text);

        List<CmsPage> getAllPrivacy();

        List<Banner> GetBanners();

        bool matchUserAndMissionSkills(long userId, long missionId);

        bool isCountryDeleted(long id);

        bool isCityDeleted(long id);

        bool isThemeDeleted(long id);

        bool isUniqueCountry(string countryName);

        bool isUniqueCity(string cityName, long countryId);

        bool isUniqueMissionTheme(string themeName);

        bool isUniqueSkill(string skillName);
        bool isUniqueCountryEdit(long id, string countryName);

        bool isUniqueCityEdit(long id, string cityName, long countryId);

        bool isUniqueMissionThemeEdit(long id, string themeName);

        bool isUniqueSkillEdit(long id, string skillName);


        // For Notifications

        List<UserNotification> GetOlderNotifications(int userid);

        List<UserNotification> GetNewerNotifications(int userid);

        void UpdateNotificationStatusById(long usernotificationid);

        NotificationSettings GetNotificationSettingsById(int userid);

        void UpdateNotificationSettingsByUser(NotificationSettings notificationSettings);

        void DoAllSettingInactive(NotificationSettings notificationSettings);

        void DeleteNotificationsByUser(int userid);

        //void AddNotificationSettingsByUser(int userid);

        string getMissionTitleById(int missionId);

        string getStoryTitleById(int storyId);

        void InserNotification(Notification notification);

        NotificationSettings GetNotificationSettingsByUser(int userid);

        int getTotalNotificationByUser(int userId);
    }
}
