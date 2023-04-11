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
    public class AdminApproveDeclineRepository : IAdminApproveDeclineRepository
    {
        private readonly CiPlatformContext _db;

        public AdminApproveDeclineRepository(CiPlatformContext db)
        {
            _db = db;
        }

        public List<MissionApplicatoin> GetMissionApplications(string query, int recSkip, int recTake)
        {
            return _db.MissionApplicatoins.Where(x => x.ApprovalStatus == "PENDING").Include(x => x.Mission).Include(x => x.User).Where(x => x.Mission.Title.Contains(query) || (x.User.FirstName + " " + x.User.LastName).Contains(query) || x.User.LastName.Contains(query)).ToList();
        }

        public int GetTotalMissionApplicationRecord(string query)
        {
            return _db.MissionApplicatoins.Where(x => x.ApprovalStatus == "PENDING").Include(x => x.Mission).Include(x => x.User).Where(x => x.Mission.Title.Contains(query) || (x.User.FirstName + " " + x.User.LastName).Contains(query) || x.User.LastName.Contains(query)).Count();
        }

        public void UpdateMissionApplicationStatus(MissionApplicatoin applicatoin)
        {
            _db.MissionApplicatoins.Update(applicatoin);
        }

        public MissionApplicatoin GetMissionApplicationById(long id)
        {
            return _db.MissionApplicatoins.Where(x => x.MissionApplicationId == id).Include(x => x.Mission).Include(x => x.User).FirstOrDefault();
        }

        public List<Story> GetStories(string query, int recSkip, int recTake)
        {
            return _db.Stories.Where(x => x.Status == "PENDING").Include(x => x.Mission).Include(x => x.User).Where(x => x.Title.Contains(query) || x.Mission.Title.Contains(query) || (x.User.FirstName + " " + x.User.LastName).Contains(query) || x.User.LastName.Contains(query)).ToList();
        }

        public int GetTotalStoriesRecord(string query)
        {
            return _db.Stories.Where(x => x.Status == "PENDING").Include(x => x.Mission).Include(x => x.User).Where(x => x.Title.Contains(query) || x.Mission.Title.Contains(query) || (x.User.FirstName + " " + x.User.LastName).Contains(query) || x.User.LastName.Contains(query)).Count();
        }

        public void UpdateStoryStatus(Story story)
        {
            _db.Stories.Update(story);
        }

        public void DeleteStory(Story story)
        {
            _db.Stories.Remove(story);
        }

        public Story GetStoryById(long id)
        {
            return _db.Stories.Where(x => x.StoryId == id).Include(x => x.Mission).Include(x => x.User).FirstOrDefault();
        }
    }
}
