using CI_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repositories.Interfaces
{
    public interface IAdminMissionThemeRepository
    {
        List<MissionTheme> GetMissionThemes(string query, int recSkip, int recTake);

        int GetTotalMissionThemeRecord(string query);

        void InsertMissionTheme(MissionTheme theme);

        void UpdateMissionTheme(MissionTheme theme);

        void DeleteMissionTheme(MissionTheme theme);

        MissionTheme GetMissionThemesById(long id);
    }
}
