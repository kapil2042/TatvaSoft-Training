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
            return _db.Missions.Where(x => x.MissionId == id).Include(x => x.MissionDocuments).Include(x => x.MissionMedia).Include(x => x.MissionSkills).FirstOrDefault();
        }

        public List<MissionSkill> GetSkillByMissionId(long missionId)
        {
            return _db.MissionSkills.Where(x => x.MissionId == missionId).Include(x => x.Skill).ToList();
        }

        public List<MissionMedium> GetMissionMediaByMissionId(long missionId)
        {
            return _db.MissionMedia.Where(x => x.MissionId == missionId).ToList();
        }

        public List<MissionDocument> GetMissionDocumentsByMissionId(long missionId)
        {
            return _db.MissionDocuments.Where(x => x.MissionId == missionId).ToList();
        }

        public void RemoveMissionSkillsBySkillIdAndMissionId(int skillId, long missionId)
        {
            _db.MissionSkills.Remove(_db.MissionSkills.Where(x => x.SkillId == skillId && x.MissionId == missionId).FirstOrDefault());
        }

        public void DeleteMissionImage(MissionMedium mm)
        {
            _db.MissionMedia.Remove(mm);
        }

        public void DeleteMissionDoc(MissionDocument md)
        {
            _db.MissionDocuments.Remove(md);
        }

        public GoalMission getGoalMissionByMissionId(long missionId)
        {
            return _db.GoalMissions.Where(x => x.MissionId == missionId).FirstOrDefault();
        }

        public void UpdateGoalMission(GoalMission goalMission)
        {
            _db.GoalMissions.Update(goalMission);
        }
    }
}
