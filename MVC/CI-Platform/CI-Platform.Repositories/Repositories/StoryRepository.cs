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
    public class StoryRepository : IStoryRepository
    {
        private readonly CiPlatformContext _db;

        public StoryRepository(CiPlatformContext db)
        {
            _db = db;
        }

        public List<Story> GetStoryList(string? userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return _db.Stories.Where(x => x.Status == "PUBLISHED").Include(x => x.Mission).ToList();
            }
            else
            {
                return _db.Stories.Where(x => x.UserId == Convert.ToInt64(userId) && x.Status == "DRAFT" || x.Status == "PUBLISHED").Include(x => x.Mission).ToList();
            }
        }

        public List<Mission> GetMissionByUserApply(int id)
        {
            return _db.Missions.Where(x => (_db.MissionApplicatoins.Where(x => x.UserId == id).Select(x => x.MissionId).ToList()).Contains(x.MissionId)).ToList();
        }

        public void InsertStory(Story story)
        {
            _db.Add(story);
        }

        public Story GetStoryById(int id)
        {
            return _db.Stories.Where(x => x.StoryId == id).Include(x => x.Mission).FirstOrDefault();
        }

        public List<StoryMedium> GetStoryMediaList(int id)
        {
            return _db.StoryMedia.Where(x => x.StoryId == id).ToList();
        }

        public void InserStoryInvitation(StoryInvite invite)
        {
            _db.StoryInvites.Add(invite);
        }
    }
}
