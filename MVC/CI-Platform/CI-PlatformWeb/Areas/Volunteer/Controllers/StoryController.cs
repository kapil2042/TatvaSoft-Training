using CI_Platform.Models;
using CI_Platform.Models.ViewModels;
using CI_Platform.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Security.Claims;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers
{
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
        [AllowAnonymous]
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

        [Authorize]
        public IActionResult ShareStory()
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            ViewBag.MissionId = new SelectList(_storyRepository.GetMissionByUserApply(Convert.ToInt32(uid)), "MissionId", "Title");
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult ShareStory(Story story, IFormFileCollection? myfile, long mission, string action)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;

            Story newStory = new Story();

            newStory.UserId = Convert.ToInt64(uid);

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
                        newStory.StoryMedia.Add(sm);
                        file.CopyTo(stream);
                    }
                }
            }
            newStory.Title = story.Title;
            newStory.MissionId = (long)mission;
            newStory.ShortDescription = story.ShortDescription;
            newStory.Description = WebUtility.HtmlEncode(story.Description);

            if (action.Equals("Submit"))
            {
                newStory.Status = "PENDING";
                newStory.PublishedAt = DateTime.Now;
            }

            ModelState.Remove("User");
            ModelState.Remove("Mission");
            if (ModelState.IsValid)
            {
                _storyRepository.InsertStory(newStory);
                _commonRepository.Save();
                return RedirectToAction("Story", "Story", new { Area = "Volunteer" });
            }
            ViewBag.MissionId = new SelectList(_storyRepository.GetMissionByUserApply(Convert.ToInt32(uid)), "MissionId", "Title", story.MissionId);
            return View(story);
        }

        [Authorize]
        public IActionResult StoryDetails(int id)
        {
            return View();
        }

    }
}
