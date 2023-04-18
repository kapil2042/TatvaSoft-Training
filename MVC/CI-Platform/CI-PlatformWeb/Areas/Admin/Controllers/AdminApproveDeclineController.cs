using CI_Platform.Models.ViewModels;
using CI_Platform.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class AdminApproveDeclineController : Controller
    {
        private readonly IAdminApproveDeclineRepository _adminApproveDeclineRepository;
        private readonly ICommonRepository _commonRepository;

        public AdminApproveDeclineController(IAdminApproveDeclineRepository AdminApproveDeclineRepository, ICommonRepository commonRepository)
        {
            _adminApproveDeclineRepository = AdminApproveDeclineRepository;
            _commonRepository = commonRepository;
        }

        public IActionResult MissionApplication(string id, int pg)
        {
            if (id == null)
            {
                id = "";
            }
            TempData["pg"] = pg;
            if (pg < 1)
            {
                pg = 1;
            }
            const int pageSize = 10;
            int recsCount = _adminApproveDeclineRepository.GetTotalMissionApplicationRecord(id);
            var pager = new VMPager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var MissionAppData = _adminApproveDeclineRepository.GetMissionApplications(id, recSkip, pager.PageSize);
            if (MissionAppData.Count() == 0 && pg > 1)
            {
                return RedirectToAction("MissionApplication", "AdminApproveDecline", new { Area = "Admin", id = id, pg = Convert.ToInt32(TempData["pg"]) - 1 });
            }
            if (TempData["msg"] != null)
                ViewBag.success = TempData["msg"];
            ViewBag.pager = pager;
            ViewBag.query = id;
            return View(MissionAppData);
        }

        public IActionResult ChangeMissionApplicationStatus(long id, bool action)
        {
            var application = _adminApproveDeclineRepository.GetMissionApplicationById(id);
            application.UpdatedAt = DateTime.Now;
            if (action)
            {
                application.ApprovalStatus = "APPROVE";
                TempData["msg"] = "Mission Application Approved successfully!";
            }
            else
            {
                application.ApprovalStatus = "DECLINE";
                TempData["msg"] = "Mission Application Declined successfully!";
            }
            _adminApproveDeclineRepository.UpdateMissionApplicationStatus(application);
            _commonRepository.Save();
            return RedirectToAction("MissionApplication", "AdminApproveDecline", new { Area = "Admin", pg = TempData["pg"] });
        }

        public IActionResult Story(string id, int pg)
        {
            if (id == null)
            {
                id = "";
            }
            TempData["pg"] = pg;
            if (pg < 1)
            {
                pg = 1;
            }
            const int pageSize = 10;
            int recsCount = _adminApproveDeclineRepository.GetTotalStoriesRecord(id);
            var pager = new VMPager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var StoryData = _adminApproveDeclineRepository.GetStories(id, recSkip, pager.PageSize);
            if (StoryData.Count() == 0 && pg > 1)
            {
                return RedirectToAction("Story", "AdminApproveDecline", new { Area = "Admin", id = id, pg = Convert.ToInt32(TempData["pg"]) - 1 });
            }
            if (TempData["msg"] != null)
                ViewBag.success = TempData["msg"];
            ViewBag.pager = pager;
            ViewBag.query = id;
            return View(StoryData);
        }

        public IActionResult ChangeStoryStatus(long id, bool action)
        {
            var story = _adminApproveDeclineRepository.GetStoryById(id);
            story.UpdatedAt = DateTime.Now;
            if (action)
            {
                story.Status = "PUBLISHED";
                TempData["msg"] = "Story Approved successfully!";
            }
            else
            {
                story.Status = "DECLINE";
                TempData["msg"] = "Story Declined successfully!";
            }
            _adminApproveDeclineRepository.UpdateStoryStatus(story);
            _commonRepository.Save();
            return RedirectToAction("Story", "AdminApproveDecline", new { Area = "Admin", pg = TempData["pg"] });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteStory(long id)
        {
            var storyMedia = _adminApproveDeclineRepository.GetStoryMediumByStoryId(id);
            foreach (var media in storyMedia)
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/storyimages", media.MediaPath);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                _adminApproveDeclineRepository.DeleteStoryMedia(media);
            }
            _adminApproveDeclineRepository.DeleteStoryInviteByStoryId(id);
            var story = _adminApproveDeclineRepository.GetStoryById(id);
            if (story != null)
            {
                _adminApproveDeclineRepository.DeleteStory(story);
            }
            _commonRepository.Save();
            TempData["msg"] = "Record Deleted Successfully!";
            return RedirectToAction("Story", "AdminApproveDecline", new { Area = "Admin", pg = TempData["pg"] });
        }


        public IActionResult User(string id, int pg)
        {
            if (id == null)
            {
                id = "";
            }
            TempData["pg"] = pg;
            if (pg < 1)
            {
                pg = 1;
            }
            const int pageSize = 10;
            int recsCount = _adminApproveDeclineRepository.GetTotalUsersRecord(id);
            var pager = new VMPager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var userData = _adminApproveDeclineRepository.GetUsers(id, recSkip, pager.PageSize);
            if (userData.Count() == 0 && pg > 1)
            {
                return RedirectToAction("User", "AdminApproveDecline", new { Area = "Admin", id = id, pg = Convert.ToInt32(TempData["pg"]) - 1 });
            }
            if (TempData["msg"] != null)
                ViewBag.success = TempData["msg"];
            ViewBag.pager = pager;
            ViewBag.query = id;
            return View(userData);
        }

        public IActionResult ChangeUserStatus(long id)
        {
            var user = _adminApproveDeclineRepository.GetUserById(id);
            user.UpdatedAt = DateTime.Now;
            if (user.Status == 0)
            {
                user.Status = 1;
                TempData["msg"] = "User Activate!";
            }
            else
            {
                user.Status = 0;
                TempData["msg"] = "User Deactivate!";
            }
            _commonRepository.UpdateUser(user);
            _commonRepository.Save();
            return RedirectToAction("User", "AdminApproveDecline", new { Area = "Admin", pg = TempData["pg"] });
        }


        public IActionResult Comments(string id, int pg)
        {
            if (id == null)
            {
                id = "";
            }
            TempData["pg"] = pg;
            if (pg < 1)
            {
                pg = 1;
            }
            const int pageSize = 10;
            int recsCount = _adminApproveDeclineRepository.GetTotalCommets(id);
            var pager = new VMPager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = _adminApproveDeclineRepository.GetComments(id, recSkip, pager.PageSize);
            if (data.Count() == 0 && pg > 1)
            {
                return RedirectToAction("Comments", "AdminApproveDecline", new { Area = "Admin", id = id, pg = Convert.ToInt32(TempData["pg"]) - 1 });
            }
            if (TempData["msg"] != null)
                ViewBag.success = TempData["msg"];
            ViewBag.pager = pager;
            ViewBag.query = id;
            return View(data);
        }

        public IActionResult ChangeCommentStatus(long id, bool action)
        {
            var comment = _adminApproveDeclineRepository.GetCommentById(id);
            comment.UpdatedAt = DateTime.Now;
            if (action)
            {
                comment.ApprovalStatus = "APPROVE";
                TempData["msg"] = "Comment Approved successfully!";
            }
            else
            {
                comment.ApprovalStatus = "DECLINE";
                TempData["msg"] = "Comment Declined successfully!";
            }
            _adminApproveDeclineRepository.UpdateComments(comment);
            _commonRepository.Save();
            return RedirectToAction("Comments", "AdminApproveDecline", new { Area = "Admin", pg = TempData["pg"] });
        }


        public IActionResult TimeSheet(string id, int pg)
        {
            if (id == null)
            {
                id = "";
            }
            TempData["pg"] = pg;
            if (pg < 1)
            {
                pg = 1;
            }
            const int pageSize = 10;
            int recsCount = _adminApproveDeclineRepository.GetTotalTimeSheetRecord(id);
            var pager = new VMPager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = _adminApproveDeclineRepository.GetTimeSheet(id, recSkip, pager.PageSize);
            if (data.Count() == 0 && pg > 1)
            {
                return RedirectToAction("TimeSheet", "AdminApproveDecline", new { Area = "Admin", id = id, pg = Convert.ToInt32(TempData["pg"]) - 1 });
            }
            if (TempData["msg"] != null)
                ViewBag.success = TempData["msg"];
            ViewBag.pager = pager;
            ViewBag.query = id;
            return View(data);
        }

        public IActionResult ChangeTimeSheetStatus(long id, bool action)
        {
            var timesheet = _adminApproveDeclineRepository.GetTimeSheetById(id);
            timesheet.UpdatedAt = DateTime.Now;
            if (action)
            {
                timesheet.Status = "APPROVED";
                TempData["msg"] = "Mission Application Approved successfully!";
            }
            else
            {
                timesheet.Status = "DECLINED";
                TempData["msg"] = "Mission Application Declined successfully!";
            }
            _adminApproveDeclineRepository.UpdateTimeSheetStatus(timesheet);
            _commonRepository.Save();
            return RedirectToAction("TimeSheet", "AdminApproveDecline", new { Area = "Admin", pg = TempData["pg"] });
        }
    }
}
