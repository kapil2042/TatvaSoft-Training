﻿using CI_Platform.Models;
using CI_Platform.Models.ViewModels;
using CI_Platform.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Security.Claims;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers
{
    [Area("Volunteer")]
    [Authorize(Policy = "VolunteerOnly")]
    public class StoryController : Controller
    {
        private readonly ICommonRepository _commonRepository;
        private readonly IStoryRepository _storyRepository;

        public StoryController(ICommonRepository commonRepository, IStoryRepository storyRepository)
        {
            _commonRepository = commonRepository;
            _storyRepository = storyRepository;
        }

        public IActionResult Story()
        {
            if (TempData["anotherstory"] != null)
            {
                ViewBag.error = TempData["anotherstory"];
            }
            if (TempData["msg"] != null)
                ViewBag.success = TempData["msg"];
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            VMStory story = new()
            {
                users = _commonRepository.GetAllUsers(),
                countries = _commonRepository.GetCountries(),
                cities = _commonRepository.GetCities(),
                skills = _commonRepository.GetSkills(),
                themes = _commonRepository.GetMissionThemes(),
                stories = _storyRepository.GetStoryList(uid),
            };
            return View(story);
        }

        [HttpPost]
        public IActionResult FilterStories(int id, string search, string country, string cities, string theme, string skill, int pg = 1)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            VMStory model = new()
            {
                users = _commonRepository.GetAllUsers(),
                countries = _commonRepository.GetCountries(),
                cities = _commonRepository.GetCities(),
                skills = _commonRepository.GetSkills(),
                themes = _commonRepository.GetMissionThemes(),
            };
            List<Story> storyDetails = _storyRepository.GetStoryList(uid);

            if (country != null)
            {
                storyDetails = storyDetails.Where(x => x.Mission.Country.Name == country).ToList();
                if (storyDetails.Count() <= 0)
                {
                    ViewBag.nostory = 1;
                }
            }

            if (cities != null)
            {
                string[] citylist = cities.Split(',', StringSplitOptions.RemoveEmptyEntries);
                storyDetails = storyDetails.Where(x => (citylist.Contains(x.Mission.City.Name))).ToList();
            }

            if (theme != null)
            {
                string[] themelist = theme.Split(',', StringSplitOptions.RemoveEmptyEntries);
                storyDetails = storyDetails.Where(x => (themelist.Contains(x.Mission.Theme.Title))).ToList();
            }

            if (skill != null)
            {
                string[] skilllist = skill.Split(',', StringSplitOptions.RemoveEmptyEntries);
                storyDetails = storyDetails.Where(x => (_commonRepository.GetMissionsIdBySkillName(skilllist)).Contains(x.MissionId)).ToList();
            }

            if (!string.IsNullOrEmpty(search))
            {
                storyDetails = storyDetails.Where(x => x.Title.ToLower().Contains(search)).ToList();
            }

            const int pageSize = 6;
            if (pg < 1)
            {
                pg = 1;
            }
            int recsCount = storyDetails.Count();
            if (recsCount <= 0)
            {
                ViewBag.nostory = 1;
            }
            ViewBag.count = recsCount;
            var pager = new VMPager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = storyDetails.Skip(recSkip).Take(pager.PageSize).ToList();
            model.stories = data;
            ViewBag.pager = pager;
            return PartialView("partiaFilterStory", model);
        }

        public IActionResult ShareStory()
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            ViewBag.MissionId = new SelectList(_commonRepository.GetMissionByUserApply(Convert.ToInt32(uid)), "MissionId", "Title");
            return View();
        }

        [HttpPost]
        public IActionResult ShareStory(Story story, IFormFileCollection? myfile, string action)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            if (myfile.Count() > 0)
            {
                ModelState.Remove("User");
                ModelState.Remove("Mission");
            }
            else
                ViewBag.imgerr = "Please upload at least one Image";
            if (ModelState.IsValid)
            {
                story.UserId = Convert.ToInt64(uid);

                foreach (IFormFile file in myfile)
                {
                    if (file != null)
                    {
                        //Set Key Name
                        string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                        //Get url To Save
                        string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/storyimages", ImageName);

                        using (var stream = new FileStream(SavePath, FileMode.Create))
                        {
                            StoryMedium sm = new StoryMedium();
                            sm.MediaType = file.ContentType.Split('/')[1].ToLower();
                            sm.MediaPath = ImageName;
                            story.StoryMedia.Add(sm);
                            file.CopyTo(stream);
                        }
                    }
                }
                story.Description = WebUtility.HtmlEncode(story.Description);
                story.Views = 0;

                if (action.Equals("Submit"))
                {
                    story.Status = "PENDING";
                    story.PublishedAt = DateTime.Now;
                }
                _storyRepository.InsertStory(story);
                _commonRepository.Save();
                return RedirectToAction("Story", "Story", new { Area = "Volunteer" });
            }
            ViewBag.MissionId = new SelectList(_commonRepository.GetMissionByUserApply(Convert.ToInt32(uid)), "MissionId", "Title", story.MissionId);
            return View(story);
        }

        [AllowAnonymous]
        public IActionResult StoryDetails(int id)
        {
            VMStoryDetails storyDetails = new VMStoryDetails();
            storyDetails.story = _storyRepository.GetStoryById(id);
            storyDetails.users = _commonRepository.GetAllUsers();
            storyDetails.storyMedia = _storyRepository.GetStoryMediaList(id);
            return View(storyDetails);
        }


        [HttpPost]
        public void Recommended_Story(int storyid, string[] mailids)
        {
            int cnt = 0;
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            var firstName = identity?.FindFirst(ClaimTypes.Name)?.Value;
            var lastName = identity?.FindFirst(ClaimTypes.Surname)?.Value;
            var link = Url.Action("StoryDetails", "Story", new { Area = "Volunteer", id = storyid });
            var mailBody = "<h1>Story For You:</h1><br> <a href='https://localhost:44304" + link + "'> <b style='color:green;'>Click Here to See Story Details</b>  </a>";
            Notification notification = new Notification();
            notification.NotificationText = firstName + " " + lastName + " - Recommends this Story - " + _commonRepository.getStoryTitleById(storyid);
            notification.FromUserId = Convert.ToInt32(uid);
            notification.NotificationType = 0;
            notification.CreatedAt = DateTime.Now;
            foreach (var mail in mailids)
            {
                long toUserId = _commonRepository.GetUserIdByEmail(mail);
                if (_commonRepository.GetNotificationSettingsByUser((int)toUserId).RecommendMission == 1)
                {
                    UserNotification userNotification = new UserNotification();
                    userNotification.UserId = toUserId;
                    userNotification.Isread = 0;
                    userNotification.CreatedAt = DateTime.Now;
                    notification.UserNotifications.Add(userNotification);
                    cnt++;
                }
                StoryInvite invite = new StoryInvite();
                invite.StoryId = storyid;
                invite.FromUserId = Convert.ToInt64(uid);
                invite.ToUserId = toUserId;
                _storyRepository.InserStoryInvitation(invite);
            }
            if (cnt != 0)
                _commonRepository.InserNotification(notification);
            _commonRepository.Save();
            _commonRepository.SendMails("Story Recommended", mailBody, mailids);
        }



        public IActionResult EditStory(int id)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            if (_storyRepository.GetStoryById(id).UserId != Convert.ToInt64(uid))
            {
                TempData["anotherstory"] = "This is not your story!!";
                return RedirectToAction("Story", "Story", new { Area = "Volunteer" });
            }
            ViewBag.MissionId = new SelectList(_commonRepository.GetMissionByUserApply(Convert.ToInt32(uid)), "MissionId", "Title");
            Story story = new Story();
            story = _storyRepository.GetStoryById(id);
            story.Description = WebUtility.HtmlDecode(story.Description);
            return View(story);
        }

        [HttpPost]
        public IActionResult EditStory(Story story1, IFormFileCollection? myfile, string action, string[] preloaded, int id)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;

            var story = _storyRepository.GetStoryById(id);

            List<StoryMedium> storyMedia = new List<StoryMedium>();
            storyMedia = _storyRepository.GetStoryMediaList(id);

            List<string> listForDelete = new List<string>();
            for (int i = 0; i < preloaded.Length; i++)
            {
                var x = preloaded[i].Split('/')[3];
                listForDelete.Add(x);
            }
            string[] str = listForDelete.ToArray();
            var storyMediaForDelete = storyMedia.Select(x => x.MediaPath).ToArray().Except(str);
            foreach (var i in storyMediaForDelete)
            {
                var img = _storyRepository.GetStoryMediaByMediaPath(i);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/storyimages", img.MediaPath);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                _storyRepository.DeleteStoryImage(img);
            }
            foreach (IFormFile file in myfile)
            {
                if (file != null)
                {
                    //Set Key Name
                    string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    //Get url To Save
                    string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/storyimages", ImageName);

                    using (var stream = new FileStream(SavePath, FileMode.Create))
                    {
                        StoryMedium sm = new StoryMedium();
                        sm.MediaType = file.ContentType.Split('/')[1].ToLower();
                        sm.MediaPath = ImageName;
                        story.StoryMedia.Add(sm);
                        file.CopyTo(stream);
                    }
                }
            }
            story.Title = story1.Title;
            story.MissionId = story1.MissionId;
            story.ShortDescription = story1.ShortDescription;
            story.Description = WebUtility.HtmlEncode(story1.Description);
            story.UpdatedAt = DateTime.Now;
            story.Views = 0;


            if (action.Equals("Submit"))
            {
                story.Status = "PENDING";
                story.PublishedAt = DateTime.Now;
                TempData["msg"] = "Story Submited Successfully! Story will display after admin approval!";
            }
            else
            {
                story.Status = "DRAFT";
            }

            ModelState.Remove("User");
            ModelState.Remove("Mission");
            if (ModelState.IsValid)
            {
                _storyRepository.UpdateStory(story);
                _commonRepository.Save();
                return RedirectToAction("Story", "Story", new { Area = "Volunteer" });
            }
            ViewBag.MissionId = new SelectList(_commonRepository.GetMissionByUserApply(Convert.ToInt32(uid)), "MissionId", "Title", story.MissionId);
            return View(story);
        }


        [HttpPost]
        public void IncrementStoryView(int storyId)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            var story = _storyRepository.GetStoryById(storyId);
            if (story != null)
            {
                if (story.UserId != Convert.ToInt64(uid))
                {
                    story.Views++;
                    _storyRepository.UpdateStory(story);
                    _commonRepository.Save();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Deletestory(long id)
        {
            var storyMedia = _storyRepository.GetStoryMediumByStoryId(id);
            foreach (var media in storyMedia)
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/storyimages", media.MediaPath);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                _storyRepository.DeleteStoryImage(media);
            }
            _storyRepository.DeleteStoryInviteByStoryId(id);
            var story = _storyRepository.GetStoryById(Convert.ToInt32(id));
            if (story != null)
            {
                _storyRepository.DeleteStory(story);
            }
            _commonRepository.Save();
            TempData["msg"] = "Story Deleted Successfully!";
            return RedirectToAction("Story", "Story", new { Area = "Volunteer" });
        }
    }
}
