using CI_Platform.Models;
using CI_Platform.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using CI_Platform.Models.ViewModels;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers
{
    [Area("Volunteer")]
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly ILoginRepository _loginRepository;
        private readonly ICommonRepository _commonRepository;

        public LoginController(ILoginRepository loginRepository, ICommonRepository commonRepository)
        {
            _loginRepository = loginRepository;
            _commonRepository = commonRepository;
        }

        public IActionResult Index(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (TempData["Registration"] != null)
                ViewBag.success = TempData["Registration"];
            if (TempData["resetpass"] != null)
                ViewBag.success = TempData["resetpass"];
            if (TempData["mailsent"] != null)
                ViewBag.success = TempData["mailsent"];
            ViewBag.banner = _commonRepository.GetBanners();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(User user, string ReturnUrl)
        {
            var userdata = _loginRepository.getUserByEmail(user.Email);
            var admin = _commonRepository.getAdminByEmail(user.Email);
            if (userdata != null)
            {
                if (userdata.Status == 1)
                {
                    bool isValid = (userdata.Email.Equals(user.Email) && _commonRepository.Decode(userdata.Password).Equals(user.Password));
                    if (isValid)
                    {
                        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, user.Email) },
                            CookieAuthenticationDefaults.AuthenticationScheme);
                        identity.AddClaim(new Claim(ClaimTypes.Name, userdata.FirstName));
                        identity.AddClaim(new Claim(ClaimTypes.Surname, userdata.LastName));
                        identity.AddClaim(new Claim(ClaimTypes.Sid, Convert.ToString(userdata.UserId)));
                        identity.AddClaim(new Claim("UserRole", "Volunteer"));
                        if (userdata.Avatar != null)
                            identity.AddClaim(new Claim(ClaimTypes.Thumbprint, userdata.Avatar));
                        var principle = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);
                        HttpContext.Session.SetString("Email", user.Email);
                        if (ReturnUrl != null)
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Mission", new { Area = "Volunteer" });
                        }
                    }
                    else
                    {
                        ViewBag.error = "Your Password is Wrong!";
                    }
                }
                else
                {
                    ViewBag.error = "User is no active longer!";
                }
            }
            else if (admin != null)
            {
                bool isValid = (admin.Email.Equals(user.Email) && _commonRepository.Decode(admin.Password).Equals(user.Password));
                if (isValid)
                {
                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, user.Email) },
                        CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, admin.FisrtName));
                    identity.AddClaim(new Claim(ClaimTypes.Surname, admin.LastName));
                    identity.AddClaim(new Claim("UserRole", "Admin"));
                    var principle = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);
                    HttpContext.Session.SetString("Email", user.Email);
                    return RedirectToAction("User", "AdminApproveDecline", new { Area = "Admin" });
                }
                else
                {
                    ViewBag.error = "Your Password is Wrong!";
                }
            }
            else
            {
                ViewBag.error = "Email Id not Found!";
            }
            ViewBag.banner = _commonRepository.GetBanners();
            return View();
        }

        public IActionResult Registration()
        {
            ViewBag.banner = _commonRepository.GetBanners();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(VMUserRegistration user)
        {
            if (ModelState.IsValid)
            {
                if (_loginRepository.getUserByEmail(user.Email) != null || _commonRepository.getAdminByEmail(user.Email) != null)
                {
                    ViewBag.error = user.Email + " was already Registered!";
                }
                else
                {
                    _loginRepository.InsertUser(user);
                    _loginRepository.Save();
                    TempData["Registration"] = "User Registred Successfully! Please Login!";
                    return RedirectToAction("Index", "Login", new { Area = "Volunteer" });
                }
                ViewBag.banner = _commonRepository.GetBanners();
                return View();
            }
            else
            {
                ViewBag.banner = _commonRepository.GetBanners();
                return View(user);
            }
        }

        public IActionResult ResetPass(string email, string token)
        {
            var dataToken = _loginRepository.getTokenByEmail(_commonRepository.Decode(email));
            if (dataToken != null && dataToken.Used == 0)
            {
                var date1 = DateTime.Now;
                var date2 = date1.AddHours(-4);
                if (dataToken.UserToken1 == token && dataToken.GeneratedAt > date2 && dataToken.GeneratedAt < date1)
                {
                    ViewBag.email = _commonRepository.Decode(email);
                    ViewBag.token = token;
                    ViewBag.banner = _commonRepository.GetBanners();
                    return View();
                }
            }
            TempData["resetpass"] = "Something was changed in Url or Url was expired! Please try again!";
            return RedirectToAction("ForgotPass", "Login", new { Area = "Volunteer" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPass(string email, string pass, string token)
        {
            var user = _loginRepository.getUserByEmail(email);
            if (user != null)
            {
                var dataToken = _loginRepository.getTokenByEmail(email);
                if (dataToken.UserToken1 == token)
                {
                    user.Password = _commonRepository.Encode(pass);
                    user.UpdatedAt = DateTime.Now;
                    dataToken.Used = 1;
                    _commonRepository.UpdateUser(user);
                    _loginRepository.UpdateToken(dataToken);
                    _loginRepository.Save();
                    TempData["resetpass"] = "Password is changed successfully!";
                    return RedirectToAction("Index", "Login", new { Area = "Volunteer" });
                }
            }
            var admin = _commonRepository.getAdminByEmail(email);
            if (admin != null)
            {
                var dataToken = _loginRepository.getTokenByEmail(email);
                if (dataToken.UserToken1 == token)
                {
                    admin.Password = _commonRepository.Encode(pass);
                    admin.UpdatedAt = DateTime.Now;
                    dataToken.Used = 1;
                    _commonRepository.UpdateAdmin(admin);
                    _loginRepository.UpdateToken(dataToken);
                    _loginRepository.Save();
                    TempData["resetpass"] = "Password is changed successfully!";
                    return RedirectToAction("Index", "Login", new { Area = "Volunteer" });
                }
            }
            ViewBag.error = "Something went Wrong! Please try again!";
            ViewBag.banner = _commonRepository.GetBanners();
            return View();
        }

        public IActionResult ForgotPass()
        {
            if (TempData["resetpass"] != null)
                ViewBag.error = TempData["resetpass"];
            ViewBag.banner = _commonRepository.GetBanners();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPass(string email)
        {
            var userdata = _loginRepository.getUserByEmail(email);
            var admin = _commonRepository.getAdminByEmail(email);
            if (userdata != null || admin != null)
            {
                var dataToken = _loginRepository.getTokenByEmail(email);
                string token = _loginRepository.TokenGenerate();
                UserToken emailtoken = new UserToken();
                if (dataToken != null)
                {
                    dataToken.UserToken1 = token;
                    dataToken.Used = 0;
                    dataToken.GeneratedAt = DateTime.Now;
                    _loginRepository.UpdateToken(dataToken);
                }
                else
                {
                    emailtoken.Email = email;
                    emailtoken.UserToken1 = token;
                    emailtoken.Used = 0;
                    emailtoken.GeneratedAt = DateTime.Now;
                    _loginRepository.InsertToken(emailtoken);
                }
                _loginRepository.Save();
                var link = Url.Action("ResetPass", "Login", new { Area = "Volunteer", email = _commonRepository.Encode(email), token = token });
                var mailBody = "<h1>Reset Password Link:</h1><br> <a href='https://localhost:44304" + link + "'> <b style='color:red;'>Click Here to Forgot Password</b>  </a>";
                _loginRepository.SendMail("Reset Password ~ CI-Platform", mailBody, email);
                TempData["mailsent"] = "Mail sent Successfully! Plese check mail";
                return RedirectToAction("Index", "Login", new { Area = "Volunteer" });
            }
            else
            {
                ViewBag.error = "Email Not Found";
                ViewBag.banner = _commonRepository.GetBanners();
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["Logout"] = "Logout Successfully";
            return RedirectToAction("Index", "Mission", new { Area = "Volunteer" });
        }
    }
}
