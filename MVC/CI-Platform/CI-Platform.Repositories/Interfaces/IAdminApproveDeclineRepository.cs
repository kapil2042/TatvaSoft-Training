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

        List<StoryMedium> GetStoryMediumByStoryId(long id);

        void DeleteStoryMedia(StoryMedium storyMedium);

        void DeleteStoryInviteByStoryId(long id);

        List<Comment> GetComments(string query, int recSkip, int recTake);

        int GetTotalCommets(string query);

        void UpdateComments(Comment comment);

        Comment GetCommentById(long id);

        List<Timesheet> GetTimeSheet(string query, int recSkip, int recTake);

        int GetTotalTimeSheetRecord(string query);

        void UpdateTimeSheetStatus(Timesheet timesheet);

        Timesheet GetTimeSheetById(long id);

        bool isSeatAvailable(long missionId);
    }
}
