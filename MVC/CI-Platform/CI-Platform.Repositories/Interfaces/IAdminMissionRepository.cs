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
    }
}
