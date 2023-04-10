using CI_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repositories.Interfaces
{
    public interface IAdminMissionApplicationRepository
    {
        List<MissionApplicatoin> GetMissionApplications(int recSkip, int recTake);

        int GetTotalMissionApplicationRecord();

        void UpdateMissionApplicationStatus(MissionApplicatoin applicatoin);

        MissionApplicatoin GetMissionApplicationById(long id);
    }
}
