using CI_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repositories.Interfaces
{
    public interface ITimeSheetRepository
    {
        Timesheet GetTimeSheetDataById(long id);
        List<Timesheet> GetTimeSheetDataByUserId(long userId);
        void InsertTimesheet(Timesheet timesheet);
        void UpdateTimesheet(Timesheet timesheet);
        void DeleteTimesheet(Timesheet timesheet);
    }
}
