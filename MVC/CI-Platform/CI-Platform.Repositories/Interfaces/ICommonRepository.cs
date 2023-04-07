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

        List<User> GetAllUsers();

        User GetUserById(long id);

        List<Country> GetCountries();

        List<City> GetCities();

        List<City> GetCitiesBycountry(int country);

        List<MissionTheme> GetMissionThemes();

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
    }
}
