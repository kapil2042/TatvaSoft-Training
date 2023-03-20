using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models.ViewModels
{
    public class VMMissionVol
    {
        public List<User> users { get; set; }
        public List<Country>? countries { get; set; }
        public List<City>? cities { get; set; }
        public List<MissionTheme>? themes { get; set; }
        public List<Skill>? skills { get; set; }
        public Mission mission { get; set; }
        public GoalMission? goalMissions { get; set; }
        public List<Timesheet>? timesheet { get; set; }
        public List<MissionMedium>? missionMedia { get; set; }
        public List<MissionSkill>? missionSkills { get; set; }
        public MissionRating? missionRating { get; set; }
        public double sum { get; set; }
        public int total { get; set; }
        public FavoriteMission? favoriteMission { get; set; }
        public MissionApplicatoin missionApplicatoin { get; set; }
        public List<MissionApplicatoin> volunteerDetails { get; set; }
        public List<MissionDocument> missionDocuments { get; set; }
        public List<Comment> comments { get; set; }
    }
}
