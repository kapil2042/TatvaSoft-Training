using CI_Platform.Data;
using CI_Platform.Models;
using CI_Platform.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CiPlatformContext _db;

        public UserRepository(CiPlatformContext db)
        {
            _db = db;
        }

        public void UpdateUserData(User user)
        {
            _db.Users.Update(user);
        }

        public void RemoveUserSkillsBySkillIdAndUserId(int skillId, long userId)
        {
            _db.UserSkills.Remove(_db.UserSkills.Where(x => x.SkillId == skillId && x.UserId == userId).FirstOrDefault());
        }
    }
}
