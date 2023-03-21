using CI_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repositories.Interfaces
{
    public interface IHomeRepository
    {
        List<User> GetAllUsers();

        List<Country> GetCountries();

        List<City> GetCities();

        List<City> GetCitiesBycountry(int country);

        List<MissionTheme> GetMissionThemes();

        List<Skill> GetSkills();

        void Save();

        void SendMails(string body, string[] mailids);

        long[] GetMissionsIdBySkillName(string[] skill);
    }
}
