using CI_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repositories.Interfaces
{
    public interface IAdminBannerRepository
    {
        List<Banner> GetBanners(string query, int recSkip, int recTake);

        int GetTotalBannerRecord(string query);

        void InsertBanner(Banner banner);

        void UpdateBanner(Banner banner);

        void DeleteBanner(Banner banner);

        Banner GetBannerById(long id);
    }
}
