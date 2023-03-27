using CI_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repositories.Interfaces
{
    public interface IStoryRepository
    {
        List<Story> GetStoryList(string? userId);
        List<Mission> GetMissionByUserApply(int id);
        void InsertStory(Story story);
        void UpdateStory(Story story);
        Story GetStoryById(int id);
        List<StoryMedium> GetStoryMediaList(int id);
        void InserStoryInvitation(StoryInvite invite);
        void DeleteStoryImage(StoryMedium sm);
    }
}
