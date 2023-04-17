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
    public class AdminRepository : IAdminRepository
    {
        private readonly CiPlatformContext _db;

        public AdminRepository(CiPlatformContext db)
        {
            _db = db;
        }
    }
}
