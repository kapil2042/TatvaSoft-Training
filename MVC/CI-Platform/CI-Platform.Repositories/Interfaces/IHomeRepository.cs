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
        List<Country> GetCountries();

        List<City> GetCities();

        List<City> GetCitiesBycountry(int country);

        List<MissionTheme> GetMissionThemes();

        List<Skill> GetSkills();

        List<Mission> GetMissions();

        Mission GetMissionsById(int id);

        List<GoalMission> GetGoalMissions();

        List<Timesheet> GetTimeSheet();

        List<MissionSkill> GetMissionSkills();

        List<Mission> GetMissionsBySearch(string s);

        List<MissionRating> GetMissionsRating();

        List<MissionMedium> GetMissionMedia();

        List<MissionApplicatoin> GetMissionApplicatoinsByUserId(int id);
    }
}
