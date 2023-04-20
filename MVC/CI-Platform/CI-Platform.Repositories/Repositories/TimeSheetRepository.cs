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
    public class TimeSheetRepository : ITimeSheetRepository
    {
        private readonly CiPlatformContext _db;

        public TimeSheetRepository(CiPlatformContext db)
        {
            _db = db;
        }

        public Timesheet GetTimeSheetDataById(long id)
        {
            return _db.Timesheets.Where(x => x.TimesheetId == id).FirstOrDefault();
        }

        public List<Timesheet> GetTimeSheetDataByUserId(long userId)
        {
            return _db.Timesheets.Where(x => x.UserId == userId && x.Status == "APPROVED").OrderByDescending(x => x.CreatedAt).Include(x => x.Mission).ToList();
        }

        public void InsertTimesheet(Timesheet timesheet)
        {
            _db.Timesheets.Add(timesheet);
        }

        public void UpdateTimesheet(Timesheet timesheet)
        {
            _db.Timesheets.Update(timesheet);
        }

        public void DeleteTimesheet(Timesheet timesheet)
        {
            _db.Timesheets.Remove(timesheet);
        }

        public List<Mission> GetMissionByUserApplyAndAppApproved(long id)
        {
            return _db.Missions.Where(x => (_db.MissionApplicatoins.Where(x => x.UserId == id && x.ApprovalStatus == "APPROVE").Select(x => x.MissionId).ToList()).Contains(x.MissionId)).ToList();
        }

        public bool isValidTimeSheetAction(long missionId, int oldAction, int newAction)
        {
            if (_db.Missions.Where(x => x.MissionId == missionId).FirstOrDefault().MissionType == "TIME")
            {
                return true;
            }
            else
            {
                var total = _db.Timesheets.Where(x => x.MissionId == missionId && x.Status != "DECLINED").Select(x => x.Action).Sum();
                if (_db.GoalMissions.Where(goal => goal.MissionId == missionId).FirstOrDefault().GoalValue >= (total - oldAction + newAction))
                    return true;
                else
                    return false;
            }
        }
    }
}
