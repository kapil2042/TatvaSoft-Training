using CI_Platform.Data;
using CI_Platform.Models;
using CI_Platform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repositories.Repositories
{
    public class StoryRepository : IStoryRepository
    {
        private readonly CiPlatformContext _db;

        public StoryRepository(CiPlatformContext db)
        {
            _db = db;
        }

        public List<Story> GetStoryList()
        {
            return _db.Stories.Where(x => x.Status == "PUBLISHED").Include(x => x.Mission).ToList();
        }
    }
}
