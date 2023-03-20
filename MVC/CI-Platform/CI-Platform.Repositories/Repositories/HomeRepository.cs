using CI_Platform.Data;
using CI_Platform.Models;
using CI_Platform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

        public List<User> GetAllUsers()
        {
            return _db.Users.ToList();
        }

        public List<Country> GetCountries()
        {
            return _db.Countries.ToList();
        }

        public List<City> GetCities()
        {
            return _db.Cities.ToList();
        }

        public List<City> GetCitiesBycountry(int country)
        {
            return _db.Cities.Where(x => x.CountryId == country).ToList();
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

        public List<MissionRating> GetMissionsRating()
        {
            return _db.MissionRatings.ToList();
        }

        public Mission GetMissionsById(int id)
        {
            return _db.Missions.Where(x => x.MissionId == id).FirstOrDefault();
        }

        public List<MissionApplicatoin> GetMissionApplicatoinsByUserId(int id)
        {
            return _db.MissionApplicatoins.Where(x => x.UserId == id).ToList();
        }

        public List<MissionMedium> GetMissionMedia()
        {
            return _db.MissionMedia.ToList();
        }

        public List<FavoriteMission> GetFavoriteMissionsByUserId(int id)
        {
            return _db.FavoriteMissions.Where(x => x.UserId == id).ToList();
        }

        public FavoriteMission GetFavoriteMissionsByUserIdAndMissionId(int id, int mid)
        {
            return _db.FavoriteMissions.Where(x => x.UserId == id && x.MissionId == mid).FirstOrDefault();
        }

        public GoalMission GetGoalMissionByMissionId(int id)
        {
            return _db.GoalMissions.Where(x => x.MissionId == id).FirstOrDefault();
        }

        public List<Timesheet> GetTimesheetByMissionId(int id)
        {
            return _db.Timesheets.Where(x => x.MissionId == id).ToList();
        }

        public List<MissionSkill> GetMissionSkillsByMissionId(int id)
        {
            return _db.MissionSkills.Where(x => x.MissionId == id).ToList();
        }

        public MissionRating GetMissionRatingByUserIdAndMissionId(int id, int mid)
        {
            return _db.MissionRatings.Where(x => x.MissionId == mid && x.UserId == id).FirstOrDefault();
        }

        public double GetSumOfMissionRatingByMissionId(int id)
        {
            return (double)_db.MissionRatings.Where(x => x.MissionId == id).Select(x => x.Rating).ToList().Sum();
        }

        public int GetTotalMissionRatingByMissionId(int id)
        {
            return _db.MissionRatings.Where(x => x.MissionId == id).Select(x => x.Rating).ToList().Count();
        }

        public List<MissionMedium> GetMissionMediaByMissionId(int id)
        {
            return _db.MissionMedia.Where(x => x.MissionId == id).ToList();
        }

        public MissionApplicatoin GetMissionApplicatoinByUserIdAndMissionId(int id, int mid)
        {
            return _db.MissionApplicatoins.Where(x => x.MissionId == mid && x.UserId == id).FirstOrDefault();
        }

        public void LikeMission(FavoriteMission favoriteMission)
        {
            _db.FavoriteMissions.Add(favoriteMission);
        }

        public void UnlikeMission(FavoriteMission favoriteMission)
        {
            _db.FavoriteMissions.Remove(favoriteMission);
        }

        public void Rating(MissionRating missionRating)
        {
            _db.MissionRatings.Add(missionRating);
        }

        public void UpdateRating(MissionRating missionRating)
        {
            _db.MissionRatings.Update(missionRating);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public List<MissionDocument> GetFavoriteMissionDocumentsByMissionId(int mid)
        {
            return _db.MissionDocuments.ToList();
        }

        public List<MissionApplicatoin> GetMissionApplicatoinsByMissionId(int mid)
        {
            return _db.MissionApplicatoins.Where(x => x.MissionId == mid && x.ApprovalStatus == "APPROVE").OrderByDescending(x => x.AppliedAt).Include(x => x.User).ToList();
        }

        public List<Comment> GetCommentsByMissionId(int mid)
        {
            return _db.Comments.Where(x => x.MissionId == mid && x.ApprovalStatus == "APPROVE").OrderByDescending(x => x.CreatedAt).Include(x => x.User).ToList();
        }

        public void PostComment(Comment comment)
        {
            _db.Comments.Add(comment);
        }

        public long[] GetMissionsIdBySkillName(string[] skill)
        {
            return _db.MissionSkills.Include(x => x.Skill).Where(x => skill.Contains(x.Skill.SkillName)).Select(x => x.MissionId).ToArray();
        }

        public void SendMails(string body, string[] mailids)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("lrs.aau.in@gmail.com");
                foreach(var mailid in mailids)
                {
                    message.To.Add(mailid);
                }
                message.Subject = "Reset Password ~ CI-Platform";
                message.IsBodyHtml = true;
                message.Body = body;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("lrs.aau.in@gmail.com", "nlugxyghjtuxmmqj");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
        }
    }
}
