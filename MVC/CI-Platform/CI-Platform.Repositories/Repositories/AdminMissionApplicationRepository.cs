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
    public class AdminMissionApplicationRepository : IAdminMissionApplicationRepository
    {
        private readonly CiPlatformContext _db;

        public AdminMissionApplicationRepository(CiPlatformContext db)
        {
            _db = db;
        }

        public List<MissionApplicatoin> GetMissionApplications(int recSkip, int recTake)
        {
            return _db.MissionApplicatoins.Where(x => x.ApprovalStatus == "PENDING").Include(x => x.Mission).Include(x => x.User).ToList();
        }

        public int GetTotalMissionApplicationRecord()
        {
            return _db.MissionApplicatoins.Count(x => x.ApprovalStatus == "PENDING");
        }

        public void UpdateMissionApplicationStatus(MissionApplicatoin applicatoin)
        {
            _db.MissionApplicatoins.Update(applicatoin);
        }

        public MissionApplicatoin GetMissionApplicationById(long id)
        {
            return _db.MissionApplicatoins.Where(x => x.MissionApplicationId == id).Include(x => x.Mission).Include(x => x.User).FirstOrDefault();
        }
    }
}
