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
    public class AdminBannerRepository : IAdminBannerRepository
    {
        private readonly CiPlatformContext _db;

        public AdminBannerRepository(CiPlatformContext db)
        {
            _db = db;
        }

        public List<Banner> GetBanners(string query, int recSkip, int recTake)
        {
            return _db.Banners.Where(x => x.Title.Contains(query)).OrderBy(x=>x.SortOrder).Skip(recSkip).Take(recTake).ToList();
        }

        public int GetTotalBannerRecord(string query)
        {
            return _db.Banners.Count(x => x.Title.Contains(query));
        }

        public void InsertBanner(Banner banner)
        {
            _db.Banners.Add(banner);
        }

        public void UpdateBanner(Banner banner)
        {
            _db.Banners.Update(banner);
        }

        public void DeleteBanner(Banner banner)
        {
            _db.Banners.Remove(banner);
        }

        public Banner GetBannerById(long id)
        {
            return _db.Banners.Where(x => x.BannerId == id).FirstOrDefault();
        }
    }
}
