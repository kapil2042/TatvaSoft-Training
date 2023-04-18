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

        public List<Admin> GetAdmins(string query, int recSkip, int recTake)
        {
            return _db.Admins.Where(x => (x.FisrtName + " " + x.LastName).Contains(query) || (x.LastName + " " + x.FisrtName).Contains(query) || x.LastName.Contains(query) || x.Email.Contains(query)).Skip(recSkip).Take(recTake).ToList();
        }

        public Admin GetAdminById(long id)
        {
            return _db.Admins.Where(x => x.AdminId == id).FirstOrDefault();
        }

        public int GetTotalAdminsRecord(string query)
        {
            return _db.Admins.Count(x => (x.FisrtName + " " + x.LastName).Contains(query) || (x.LastName + " " + x.FisrtName).Contains(query) || x.LastName.Contains(query) || x.Email.Contains(query));
        }

        public void InsertAdmin(Admin admin)
        {
            _db.Admins.Add(admin);
        }
    }
}
