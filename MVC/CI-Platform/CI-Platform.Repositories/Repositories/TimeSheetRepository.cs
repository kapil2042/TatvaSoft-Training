﻿using CI_Platform.Data;
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
    }
}
