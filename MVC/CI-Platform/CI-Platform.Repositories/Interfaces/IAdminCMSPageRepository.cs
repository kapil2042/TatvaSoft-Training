using CI_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repositories.Interfaces
{
    public interface IAdminCMSPageRepository
    {
        List<CmsPage> GetCmsPages(int recSkip, int recTake);

        int GetTotalCmsPageRecord();

        void InsertCmsPage(CmsPage cms);

        void UpdateCmsPage(CmsPage cms);

        void DeleteCmsPage(CmsPage cms);

        CmsPage GetCmsPageById(long id);
    }
}
