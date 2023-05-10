using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models
{
    public class UserNotification
    {
        public long UserNotificationId { get; set; }
        public long NotificationId { get; set; }
        public long UserId { get; set; }
        public int Isread { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual Notification Notification { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
