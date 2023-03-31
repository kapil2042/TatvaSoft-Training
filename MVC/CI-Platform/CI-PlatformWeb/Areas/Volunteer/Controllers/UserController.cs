using CI_Platform.Models;
using CI_Platform.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ICommonRepository _commonRepository;
        private readonly IUserRepository _userRepository;

        public UserController(ICommonRepository commonRepository, IUserRepository userRepository)
        {
            _commonRepository = commonRepository;
            _userRepository = userRepository;
        }

        public IActionResult UserProfile()
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            ViewBag.city = _commonRepository.GetCities();
            ViewBag.country = _commonRepository.GetCountries();
            ViewBag.skills = _commonRepository.GetSkills();
            return View(_commonRepository.GetUserById(Convert.ToInt64(uid)));
        }

        [HttpPost]
        public IActionResult UserProfile(User user, int[] userSkills)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            ViewBag.city = _commonRepository.GetCities();
            ViewBag.country = _commonRepository.GetCountries();
            ViewBag.skills = _commonRepository.GetSkills();

            var userUpdated = _commonRepository.GetUserById(Convert.ToInt64(uid));

            userUpdated.FirstName = user.FirstName;
            userUpdated.LastName = user.LastName;
            userUpdated.EmployeeId = user.EmployeeId;
            userUpdated.ManagerDetails = user.ManagerDetails;
            userUpdated.Title = user.Title;
            userUpdated.Department = user.Department;
            userUpdated.ProfileText = user.ProfileText;
            userUpdated.WhyIVolunteer = user.WhyIVolunteer;
            userUpdated.CityId = user.CityId;
            userUpdated.CountryId = user.CountryId;
            userUpdated.Availability = user.Availability;
            userUpdated.LinkedInUrl = user.LinkedInUrl;

            ModelState.Remove("City");
            ModelState.Remove("Email");
            ModelState.Remove("Country");
            ModelState.Remove("Password");
            ModelState.Remove("PhoneNumber");

            var userSkillOld = userUpdated.UserSkills.ToList();

            if (ModelState.IsValid)
            {
                for (int i = 0; i < userSkills.Length; i++)
                {
                    if (userSkillOld.Count() > 0)
                    {
                        foreach (var j in userSkillOld)
                        {
                            if (i != j.UserSkillId)
                            {
                                _userRepository.RemoveUserSkills(j);
                            }
                        }
                    }
                    UserSkill userSkillNew = new UserSkill();
                    userSkillNew.UserId = Convert.ToInt64(uid);
                    userSkillNew.SkillId = userSkills[i];
                    //_userRepository.AddUserSkills(userSkillNew);
                    userUpdated.UserSkills.Add(userSkillNew);
                }
                _userRepository.UpdateUserData(userUpdated);
                _commonRepository.Save();
            }
            return View(userUpdated);
        }


        [HttpPost]
        public IActionResult ChangePassword(string oldPass, string newPass, string cnfPass)
        {
            return RedirectToAction("UserProfile", "User", new { Area = "Volunteer" });
        }
    }
}
