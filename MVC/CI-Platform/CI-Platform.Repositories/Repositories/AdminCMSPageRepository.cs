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

        public List<CmsPage> GetCmsPages(string query, int recSkip, int recTake)
        {
            return _db.CmsPages.Where(x => x.Title.Contains(query)).Skip(recSkip).Take(recTake).ToList();
        }

        public int GetTotalCmsPageRecord(string query)
        {
            return _db.CmsPages.Count(x => x.Title.Contains(query));
        }

        public void InsertCmsPage(CmsPage cms)
        {
            _db.CmsPages.Add(cms);
        }

        public void UpdateCmsPage(CmsPage cms)
        {
            _db.CmsPages.Update(cms);
        }

        public void DeleteCmsPage(CmsPage cms)
        {
            _db.CmsPages.Remove(cms);
        }

        public CmsPage GetCmsPageById(long id)
        {
            return _db.CmsPages.Where(x => x.CsmPageId == id).FirstOrDefault();
        }
    }
}
