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

        public MissionMedium GetMissionMediaByMediaName(string mediaName)
        {
            return _db.MissionMedia.Where(x => x.MediaName == mediaName).FirstOrDefault();
        }

        public void DeleteGoalMission(GoalMission mission)
        {
            _db.GoalMissions.Remove(mission);
        }

        public void DeleteFavouriteMissionByMissionId(long id)
        {
            foreach (var favMission in _db.FavoriteMissions.Where(x => x.MissionId == id).ToList())
            {
                _db.FavoriteMissions.Remove(favMission);
            }
        }

        public void DeleteMissionRatingByMissionId(long id)
        {
            foreach (var rating in _db.MissionRatings.Where(x => x.MissionId == id).ToList())
            {
                _db.MissionRatings.Remove(rating);
            }
        }

        public void DeleteMissionInviteByMissionId(long id)
        {
            foreach (var invite in _db.MissionInvites.Where(x => x.MissionId == id).ToList())
            {
                _db.MissionInvites.Remove(invite);
            }
        }

        public void DeleteMissionApplicationByMissionId(long id)
        {
            foreach (var app in _db.MissionApplicatoins.Where(x => x.MissionId == id).ToList())
            {
                _db.MissionApplicatoins.Remove(app);
            }
        }

        public void DeleteMissionSkillsByMissionId(long id)
        {
            foreach (var skill in _db.MissionSkills.Where(x => x.MissionId == id).ToList())
            {
                _db.MissionSkills.Remove(skill);
            }
        }

        public void DeleteTimeSheetByMissionId(long id)
        {
            foreach (var ts in _db.Timesheets.Where(x => x.MissionId == id).ToList())
            {
                _db.Timesheets.Remove(ts);
            }
        }

        public void DeleteCommentsByMissionId(long id)
        {
            foreach (var c in _db.Comments.Where(x => x.MissionId == id).ToList())
            {
                _db.Comments.Remove(c);
            }
        }

        public void DeleteStoriesByMissionId(long id)
        {
            var story = _db.Stories.Where(x => x.MissionId == id).ToList();
            foreach (var i in story)
            {
                var storyMedia = _db.StoryMedia.Where(x => x.StoryId == i.StoryId).ToList();
                foreach (var media in storyMedia)
                {
                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/storyimages", media.MediaPath);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                    _db.StoryMedia.Remove(media);
                }
                foreach (var invite in _db.StoryInvites.Where(x => x.StoryId == i.StoryId).ToList())
                {
                    _db.StoryInvites.Remove(invite);
                }
                _db.Stories.Remove(i);
            }
        }
    }
}
