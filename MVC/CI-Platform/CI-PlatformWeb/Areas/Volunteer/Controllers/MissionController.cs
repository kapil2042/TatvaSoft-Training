﻿using CI_Platform.Models;
using CI_Platform.Models.ViewModels;
using CI_Platform.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers
{
    public class MissionController : Controller
    {
        private readonly ICommonRepository _commonRepository;
        private readonly IMissionRepository _missionRepository;

        public MissionController(ICommonRepository commonRepository, IMissionRepository missionRepository)
        {
            _commonRepository = commonRepository;
            _missionRepository = missionRepository;
        }
        public IActionResult Index()
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            if (TempData["Logout"] != null)
                ViewBag.success = TempData["Logout"];
            VMMissions model = new()
            {
                users = _commonRepository.GetAllUsers(),
                mission = _missionRepository.GetMissions(),
                countries = _commonRepository.GetCountries(),
                cities = _commonRepository.GetCities(),
                skills = _commonRepository.GetSkills(),
                themes = _commonRepository.GetMissionThemes(),
                goal = _missionRepository.GetGoalMissions(),
                timesheet = _missionRepository.GetTimeSheet(),
                missionSkills = _missionRepository.GetMissionSkills(),
                missionRatings = _missionRepository.GetMissionsRating(),
                missionMedia = _missionRepository.GetMissionMedia(),
                favoriteMission = _missionRepository.GetFavoriteMissionsByUserId(Convert.ToInt32(uid)),
                missionApplicatoin = _missionRepository.GetMissionApplicatoinsByUserId(Convert.ToInt32(uid)),
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult FilterMissions(int id, string search, string country, string cities, string theme, string skill, int pg = 1)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            VMMissions model = new()
            {
                users = _commonRepository.GetAllUsers(),
                countries = _commonRepository.GetCountries(),
                cities = _commonRepository.GetCities(),
                skills = _commonRepository.GetSkills(),
                themes = _commonRepository.GetMissionThemes(),
                goal = _missionRepository.GetGoalMissions(),
                timesheet = _missionRepository.GetTimeSheet(),
                missionSkills = _missionRepository.GetMissionSkills(),
                missionRatings = _missionRepository.GetMissionsRating(),
                missionMedia = _missionRepository.GetMissionMedia(),
                favoriteMission = _missionRepository.GetFavoriteMissionsByUserId(Convert.ToInt32(uid)),
                missionApplicatoin = _missionRepository.GetMissionApplicatoinsByUserId(Convert.ToInt32(uid)),
            };

            List<Mission> m = _missionRepository.GetMissions();

            if (country != null)
            {
                m = m.Where(x => x.Country.Name == country).ToList();
                if (m.Count() <= 0)
                {
                    ViewBag.nomission = 1;
                }
            }

            if (cities != null)
            {
                string[] citylist = cities.Split(',', StringSplitOptions.RemoveEmptyEntries);
                m = m.Where(x => (citylist.Contains(x.City.Name))).ToList();
            }

            if (theme != null)
            {
                string[] themelist = theme.Split(',', StringSplitOptions.RemoveEmptyEntries);
                m = m.Where(x => (themelist.Contains(x.Theme.Title))).ToList();
            }

            if (skill != null)
            {
                string[] skilllist = skill.Split(',', StringSplitOptions.RemoveEmptyEntries);
                m = m.Where(x => (_commonRepository.GetMissionsIdBySkillName(skilllist)).Contains(x.MissionId)).ToList();
            }

            if (!string.IsNullOrEmpty(search))
            {
                m = m.Where(x => x.Title.ToLower().Contains(search)).ToList();
            }
            switch (id)
            {
                case 1:
                    m = m.OrderBy(x => x.Title).ToList();
                    break;
                case 2:
                    m = m.OrderByDescending(x => x.Title).ToList();
                    break;
                case 3:
                    m = m.OrderBy(x => x.StartDate).ToList();
                    break;
                case 4:
                    m = m.OrderByDescending(x => x.StartDate).ToList();
                    break;
            }
            const int pageSize = 6;
            if (pg < 1)
            {
                pg = 1;
            }
            int recsCount = m.Count();
            if (recsCount <= 0)
            {
                ViewBag.nomission = 1;
            }
            ViewBag.count = recsCount;
            var pager = new VMPager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = m.Skip(recSkip).Take(pager.PageSize).ToList();
            model.mission = data;
            ViewBag.pager = pager;
            return PartialView("partialFilterMissions", model);
        }


        [Authorize]
        public IActionResult Mission_volunteering(int id)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            VMMissionVol model = new()
            {
                users = _commonRepository.GetAllUsers(),
                countries = _commonRepository.GetCountries(),
                cities = _commonRepository.GetCities(),
                skills = _commonRepository.GetSkills(),
                themes = _commonRepository.GetMissionThemes(),
                mission = _missionRepository.GetMissionsById(id),
                goalMissions = _missionRepository.GetGoalMissionByMissionId(id),
                timesheet = _missionRepository.GetTimesheetByMissionId(id),
                missionMedia = _missionRepository.GetMissionMediaByMissionId(id),
                missionSkills = _missionRepository.GetMissionSkillsByMissionId(id),
                missionRating = _missionRepository.GetMissionRatingByUserIdAndMissionId(Convert.ToInt32(uid), id),
                sum = _missionRepository.GetSumOfMissionRatingByMissionId(id),
                total = _missionRepository.GetTotalMissionRatingByMissionId(id),
                favoriteMission = _missionRepository.GetFavoriteMissionsByUserIdAndMissionId(Convert.ToInt32(uid), id),
                missionApplicatoin = _missionRepository.GetMissionApplicatoinByUserIdAndMissionId(Convert.ToInt32(uid), id),
                volunteerDetails = _missionRepository.GetMissionApplicatoinsByMissionId(id),
                missionDocuments = _missionRepository.GetFavoriteMissionDocumentsByMissionId(id),
                comments = _missionRepository.GetCommentsByMissionId(id),
            };
            return View(model);
        }


        [Authorize]
        [HttpPost]
        public void Favourite_Mission(int missoinid)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            var data = _missionRepository.GetFavoriteMissionsByUserIdAndMissionId(Convert.ToInt32(uid), missoinid);
            if (data != null)
            {
                _missionRepository.UnlikeMission(data);
            }
            else
            {
                FavoriteMission favoriteMission = new FavoriteMission();
                favoriteMission.MissionId = missoinid;
                favoriteMission.UserId = Convert.ToInt32(uid);
                _missionRepository.LikeMission(favoriteMission);
            }
            _commonRepository.Save();
        }

        [Authorize]
        [HttpPost]
        public void Rating_Mission(int missoinid, int rat)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            var data = _missionRepository.GetMissionRatingByUserIdAndMissionId(Convert.ToInt32(uid), missoinid);
            if (data != null)
            {
                data.UpdatedAt = DateTime.Now;
                data.Rating = rat;
                _missionRepository.UpdateRating(data);
            }
            else
            {
                MissionRating missionRating = new MissionRating();
                missionRating.MissionId = missoinid;
                missionRating.UserId = Convert.ToInt32(uid);
                missionRating.Rating = rat;
                _missionRepository.Rating(missionRating);
            }
            _commonRepository.Save();
        }


        [Authorize]
        [HttpPost]
        public void Post_Comment(int missoinid, string commenttext)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            Comment comment = new Comment();
            comment.UserId = Convert.ToInt32(uid);
            comment.CommentText = commenttext;
            comment.MissionId = missoinid;
            comment.ApprovalStatus = "PENDING";
            _missionRepository.PostComment(comment);
            _commonRepository.Save();
        }

        [Authorize]
        [HttpPost]
        public void Recommended_Mission(int missoinid, string[] mailids)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            var link = Url.Action("Mission_volunteering", "Mission", new { Area = "Volunteer", id = missoinid });
            var mailBody = "<h1>Mission For You:</h1><br> <a href='https://localhost:44304" + link + "'> <b style='color:green;'>Click Here to See Mission Details</b>  </a>";

            foreach(var mail in mailids)
            {
                long toUserId = _commonRepository.GetUserIdByEmail(mail);
                MissionInvite invite = new MissionInvite();
                invite.MissionId = missoinid;
                invite.FromUserId = Convert.ToInt64(uid);
                invite.ToUserId = toUserId;
                _missionRepository.InserMissionInvitation(invite);
            }
            _commonRepository.Save();
            _commonRepository.SendMails(mailBody, mailids);
        }


        [Authorize]
        [HttpPost]
        public IActionResult GetVolunteer(int mid, int pg)
        {
            List<MissionApplicatoin> list = new List<MissionApplicatoin>();

            list = _missionRepository.GetMissionApplicatoinsByMissionId(mid);

            const int pageSize = 9;
            if (pg < 1)
            {
                pg = 1;
            }
            int recsCount = list.Count();

            ViewBag.volcount = recsCount;
            var pager = new VMPager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            list = list.Skip(recSkip).Take(pager.PageSize).ToList();
            ViewBag.pager = pager;
            return PartialView("volunteerPagination", list);
        }
    }
}
