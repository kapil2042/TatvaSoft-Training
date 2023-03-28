using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models.ViewModels
{
    public class VMTimeSheet
    {
        public List<Mission>? Missions { get; set; }
        public List<Timesheet>? Timesheets { get; set; }
        public long? Id { get; set; }
        public long? missionId { get; set; }
        public DateOnly dateVolunteer { get; set; }
        public int? Hour { get; set; }
        public int? Minute { get; set; }
        public int? Action { get; set; }
        public string? Message { get; set; }
    }
}
