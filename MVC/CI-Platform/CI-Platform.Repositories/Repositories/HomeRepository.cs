using CI_Platform.Data;
using CI_Platform.Models;
using CI_Platform.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repositories.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly CiPlatformContext _db;

        public HomeRepository(CiPlatformContext db)
        {
            _db = db;
        }

        public List<City> GetCities()
        {
            return _db.Cities.ToList();
        }

        public List<Country> GetCountries()
        {
            return _db.Countries.ToList(); 
        }

        public List<Skill> GetSkills()
        {
            return _db.Skills.ToList();
        }

        public List<MissionTheme> GetMissionThemes()
        {
            return _db.MissionThemes.ToList();
        }

        public List<Mission> GetMissions()
        {
            return _db.Missions.ToList();
        }

        public List<GoalMission> GetGoalMissions()
        {
            return _db.GoalMissions.ToList();
        }

        public List<Timesheet> GetTimeSheet()
        {
            return _db.Timesheets.ToList();
        }

        public List<MissionSkill> GetMissionSkills()
        {
            return _db.MissionSkills.ToList();
        }

        public List<Mission> GetMissionsAtoZ()
        {
            return _db.Missions.OrderBy(x => x.Title).ToList();
        }

        public List<Mission> GetMissionsZtoA()
        {
            return _db.Missions.OrderByDescending(x => x.Title).ToList();
        }

        public List<Mission> GetMissionsNew()
        {
            return _db.Missions.OrderBy(x => x.CreatedAt).ToList();
        }

        public List<Mission> GetMissionsOld()
        {
            return _db.Missions.OrderByDescending(x => x.CreatedAt).ToList();
        }
    }
}
