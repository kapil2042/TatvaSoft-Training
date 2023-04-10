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
    public class AdminCMSPageRepository : IAdminCMSPageRepository
    {
        private readonly CiPlatformContext _db;

        public AdminCMSPageRepository(CiPlatformContext db)
        {
            _db = db;
        }

        public List<CmsPage> GetCmsPages(int recSkip, int recTake)
        {
            return _db.CmsPages.Skip(recSkip).Take(recTake).ToList();
        }

        public int GetTotalCmsPageRecord()
        {
            return _db.CmsPages.Count();
        }
    }
}
