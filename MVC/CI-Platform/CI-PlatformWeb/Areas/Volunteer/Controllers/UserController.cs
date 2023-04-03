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
                var SkillForAdd = userSkills.Except(userUpdated.UserSkills.Select(x => x.SkillId).ToArray());
                var SkillForDelete = userUpdated.UserSkills.Select(x => x.SkillId).ToArray().Except(userSkills);
                foreach (var i in SkillForDelete)
                {
                    _userRepository.RemoveUserSkillsBySkillIdAndUserId(i, userUpdated.UserId);
                }
                foreach (var i in SkillForAdd)
                {
                    UserSkill userSkillNew = new UserSkill();
                    userSkillNew.UserId = Convert.ToInt64(uid);
                    userSkillNew.SkillId = i;
                    userUpdated.UserSkills.Add(userSkillNew);
                }
                _userRepository.UpdateUserData(userUpdated);
                _commonRepository.Save();
            }
            return View(userUpdated);
        }


        [Authorize]
        [HttpPost]
        public bool ChangePassword(long userId, string oldPass, string newPass)
        {
            var userData = _commonRepository.GetUserById(userId);
            if (userData != null)
            {
                var tempPass = _commonRepository.Decode(userData.Password);
                if (tempPass == oldPass)
                {
                    userData.Password = _commonRepository.Encode(newPass);
                    _commonRepository.UpdateUser(userData);
                    _commonRepository.Save();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        [HttpPost]
        public IActionResult UploadImage(string image)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            var userData = _commonRepository.GetUserById(Convert.ToInt64(uid));
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", userData.Avatar);
            try
            {
                string[] imageParts = image.Split(new string[] { ";base64," }, StringSplitOptions.None);
                string[] imageTypeAux = imageParts[0].Split(new string[] { "image/" }, StringSplitOptions.None);
                string imageType = imageTypeAux[1];
                byte[] imageBytes = Convert.FromBase64String(imageParts[1]);
                string iname = Guid.NewGuid().ToString();
                string fileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/userimages", iname + ".png");
                System.IO.File.WriteAllBytes(fileName, imageBytes);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                userData.Avatar = "userimages/" + iname + ".png";
                _commonRepository.UpdateUser(userData);
                _commonRepository.Save();
                return Json(new { src = iname });
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Error occurred. " + ex.Message });
            }
        }
    }
}
