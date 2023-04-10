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
    public class AdminMissionThemeRepository : IAdminMissionThemeRepository
    {
        private readonly CiPlatformContext _db;

        public AdminMissionThemeRepository(CiPlatformContext db)
        {
            _db = db;
        }

        public List<MissionTheme> GetMissionThemes(int recSkip, int recTake)
        {
            return _db.MissionThemes.Skip(recSkip).Take(recTake).ToList();
        }

        public int GetTotalMissionThemeRecord()
        {
            return _db.MissionThemes.Count();
        }

        public void InsertMissionTheme(MissionTheme theme)
        {
            _db.MissionThemes.Add(theme);
        }

        public void UpdateMissionTheme(MissionTheme theme)
        {
            _db.MissionThemes.Update(theme);
        }

        public void DeleteMissionTheme(MissionTheme theme)
        {
            _db.MissionThemes.Remove(theme);
        }

        public MissionTheme GetMissionThemesById(long id)
        {
            return _db.MissionThemes.Where(x => x.MissionThemeId == id).FirstOrDefault();
        }
    }
}
