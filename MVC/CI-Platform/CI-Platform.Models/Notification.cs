using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models
{
    public class Notification
    {
        public long NotificationId { get; set; }
        public string NotificationText { get; set; }
        public long? FromUserId { get; set; }
        public int NotificationType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual ICollection<UserNotification> UserNotifications { get; } = new List<UserNotification>();
    }
}
