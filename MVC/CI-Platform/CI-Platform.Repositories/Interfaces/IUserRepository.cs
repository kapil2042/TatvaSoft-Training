using CI_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void UpdateUserData(User user);

        void RemoveUserSkillsBySkillIdAndUserId(int skillId, long userId);
    }
}
