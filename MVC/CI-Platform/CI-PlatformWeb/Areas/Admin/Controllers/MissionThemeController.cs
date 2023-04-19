using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CI_Platform.Data;
using CI_Platform.Models;
using Microsoft.AspNetCore.Authorization;
using CI_Platform.Repositories.Interfaces;
using CI_Platform.Models.ViewModels;

namespace CI_PlatformWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class MissionThemeController : Controller
    {
        private readonly IAdminMissionThemeRepository _adminMissionThemeRepository;
        private readonly ICommonRepository _commonRepository;

        public MissionThemeController(IAdminMissionThemeRepository missionThemeRepository, ICommonRepository commonRepository)
        {
            _adminMissionThemeRepository = missionThemeRepository;
            _commonRepository = commonRepository;
        }

        public IActionResult Index(string id, int pg)
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
            int recsCount = _adminMissionThemeRepository.GetTotalMissionThemeRecord(id);
            var pager = new VMPager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var MissionThemeData = _adminMissionThemeRepository.GetMissionThemes(id, recSkip, pager.PageSize);
            if (MissionThemeData.Count() == 0 && pg > 1)
            {
                return RedirectToAction("Index", "MissionTheme", new { Area = "Admin", id = id, pg = Convert.ToInt32(TempData["pg"]) - 1 });
            }
            if (TempData["msg"] != null)
                ViewBag.success = TempData["msg"];
            ViewBag.pager = pager;
            ViewBag.query = id;
            return View(MissionThemeData);
        }


        public IActionResult AddMissionTheme()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMissionTheme(MissionTheme missionTheme)
        {
            if (_commonRepository.isUniqueMissionTheme(missionTheme.Title))
            {
                if (ModelState.IsValid)
                {
                    _adminMissionThemeRepository.InsertMissionTheme(missionTheme);
                    _commonRepository.Save();
                    TempData["msg"] = "Record Inserted Successfully!";
                    return RedirectToAction("Index", "MissionTheme", new { Area = "Admin", pg = TempData["pg"] });
                }
            }
            else
            {
                ViewBag.error = "Theme Title with " + missionTheme.Title + " is already exists";
            }
            return View(missionTheme);
        }


        public IActionResult EditMissionTheme(long id)
        {
            var missionTheme = _adminMissionThemeRepository.GetMissionThemesById(id);
            if (missionTheme != null)
            {
                return View(missionTheme);
            }
            return RedirectToAction("Index", "MissionTheme", new { Area = "Admin", pg = TempData["pg"] });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMissionTheme(long id, MissionTheme missionTheme)
        {
            if (_commonRepository.isUniqueMissionTheme(missionTheme.Title))
            {
                var theme = _adminMissionThemeRepository.GetMissionThemesById(id);
                if (ModelState.IsValid)
                {
                    theme.UpdatedAt = DateTime.Now;
                    theme.Title = missionTheme.Title;
                    theme.Status = missionTheme.Status;
                    _adminMissionThemeRepository.UpdateMissionTheme(theme);
                    _commonRepository.Save();
                    TempData["msg"] = "Record Edited Successfully!";
                    return RedirectToAction("Index", "MissionTheme", new { Area = "Admin", pg = TempData["pg"] });
                }
            }
            else
            {
                ViewBag.error = "Theme Title with " + missionTheme.Title + " is already exists";
            }
            return View(missionTheme);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteMissionTheme(long id)
        {
            var missionTheme = _adminMissionThemeRepository.GetMissionThemesById(id);
            if (missionTheme != null)
            {
                missionTheme.DeletedAt = DateTime.Now;
                _adminMissionThemeRepository.UpdateMissionTheme(missionTheme);
            }
            _commonRepository.Save();
            TempData["msg"] = "Record Deleted Successfully!";
            return RedirectToAction("Index", "MissionTheme", new { Area = "Admin", pg = TempData["pg"] });
        }
    }
}
