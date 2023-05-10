using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models
{
    public class NotificationSettings
    {
        public long NotificationSettingsId { get; set; }
        public long UserId { get; set; }
        public int RecommendMission { get; set; }
        public int RecommendStory { get; set; }
        public int VolunteerHour { get; set; }
        public int VolunteerGoal { get; set; }
        public int CommentApprovation { get; set; }
        public int StoryApprovation { get; set; }
        public int MissionApplicationApprovation { get; set; }
        public int NewMisson { get; set; }
        public int NewMessage { get; set; }
        public int News { get; set; }
        public int FromEmail { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("NotificationSettings")]
        public virtual User User { get; set; } = null!;
    }
}
