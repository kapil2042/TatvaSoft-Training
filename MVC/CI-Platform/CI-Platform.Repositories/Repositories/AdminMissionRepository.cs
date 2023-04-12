using CI_Platform.Data;
using CI_Platform.Models;
using CI_Platform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repositories.Repositories
{
    public class AdminMissionRepository : IAdminMissionRepository
    {
        private readonly CiPlatformContext _db;

        public AdminMissionRepository(CiPlatformContext db)
        {
            _db = db;
        }

        public List<Mission> GetMissions(string query, int recSkip, int recTake)
        {
            return _db.Missions.Where(x => x.Title.Contains(query) || x.MissionType.Contains(query)).Skip(recSkip).Take(recTake).ToList();
        }

        public int GetTotalMissionsRecord(string query)
        {
            return _db.Missions.Count(x => x.Title.Contains(query) || x.MissionType.Contains(query));
        }

        public void InsertMission(Mission mission)
        {
            _db.Missions.Add(mission);
        }

        public void UpdateMission(Mission mission)
        {
            _db.Missions.Update(mission);
        }

        public void DeleteMission(Mission mission)
        {
            _db.Missions.Remove(mission);
        }

        public Mission GetMissionById(long id)
        {
            return _db.Missions.Where(x => x.MissionId == id).Include(x=>x.MissionDocuments).Include(x=>x.MissionMedia).Include(x=>x.MissionSkills).FirstOrDefault();
        }
    }
}
