using CI_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repositories.Interfaces
{
    public interface IAdminApproveDeclineRepository
    {
        List<MissionApplicatoin> GetMissionApplications(string query, int recSkip, int recTake);

        int GetTotalMissionApplicationRecord(string query);

        void UpdateMissionApplicationStatus(MissionApplicatoin applicatoin);

        MissionApplicatoin GetMissionApplicationById(long id);

        List<Story> GetStories(string query, int recSkip, int recTake);

        int GetTotalStoriesRecord(string query);

        void UpdateStoryStatus(Story story);

        void DeleteStory(Story story);

        Story GetStoryById(long id);

        List<User> GetUsers(string query, int recSkip, int recTake);

        User GetUserById(long id);

        int GetTotalUsersRecord(string query);
    }
}
