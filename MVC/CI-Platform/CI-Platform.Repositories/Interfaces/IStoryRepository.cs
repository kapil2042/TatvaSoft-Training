﻿using CI_Platform.Models;
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
        void InsertStory(Story story);
        void UpdateStory(Story story);
        Story GetStoryById(int id);
        List<StoryMedium> GetStoryMediaList(int id);
        void InserStoryInvitation(StoryInvite invite);
        void DeleteStoryImage(StoryMedium sm);
        StoryMedium GetStoryMediaByMediaPath(string mediaPath);
        List<StoryMedium> GetStoryMediumByStoryId(long id);
        void DeleteStoryInviteByStoryId(long id);
        void DeleteStory(Story story);
    }
}
