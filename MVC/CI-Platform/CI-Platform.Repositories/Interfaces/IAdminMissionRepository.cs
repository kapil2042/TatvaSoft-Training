using CI_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repositories.Interfaces
{
    public interface IAdminMissionRepository
    {
        List<Mission> GetMissions(string query, int recSkip, int recTake);

        int GetTotalMissionsRecord(string query);

        void InsertMission(Mission mission);

        void UpdateMission(Mission mission);

        void DeleteMission(Mission mission);

        Mission GetMissionById(long id);

        List<MissionSkill> GetSkillByMissionId(long missionId);

        List<MissionMedium> GetMissionMediaByMissionId(long missionId);

        List<MissionDocument> GetMissionDocumentsByMissionId(long missionId);

        void RemoveMissionSkillsBySkillIdAndMissionId(int skillId, long missionId);

        void DeleteMissionImage(MissionMedium mm);

        void DeleteMissionDoc(MissionDocument md);

        GoalMission getGoalMissionByMissionId(long missionId);

        void UpdateGoalMission(GoalMission goalMission);
    }
}
