using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models.ViewModels
{
    public class VMMissions
    {
        public List<User> users { get; set; }
        public List<Country> ?countries { get; set; }
        public List<City> ?cities { get; set; }
        public List<MissionTheme> ?themes { get; set; }
        public List<Skill> ?skills { get; set; }
        public List<Mission> ?mission { get; set; }
        public List<GoalMission> ?goal { get; set; }
        public List<Timesheet> ?timesheet { get; set; }
        public List<MissionMedium> ?missionMedia { get; set; }
        public List<MissionSkill> ?missionSkills { get; set; }
        public List<MissionRating> ?missionRatings { get; set; }
        public List<MissionApplicatoin> ?missionApplicatoin { get; set; }
        public List<MissionApplicatoin> ?missionAppAll { get; set; }
        public List<FavoriteMission> ?favoriteMission { get; set; }
    }
}
