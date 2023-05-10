using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models.ViewModels
{
    public class VMNotification
    {
        public NotificationSettings NotificationSettings { get; set; }
        public List<UserNotification> OldUserNotifications { get; set; }
        public List<UserNotification> NewUserNotifications { get; set; }
        public List<User> Users { get; set; }
    }
}
