using CI_Platform.Data;
using CI_Platform.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers
{
    public class LoginController : Controller
    {
        private readonly CiPlatformContext _db;

        public LoginController(CiPlatformContext db)
        {
            _db = db;
        }
        public IActionResult Index(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(User user, string ReturnUrl)
        {
            var userdata = _db.Users.Where(x => x.Email == user.Email).SingleOrDefault();
            if (userdata != null)
            {
                if (userdata.Status == 1)
                {
                    bool isValid = (userdata.Email.Equals(user.Email) && userdata.Password.Equals(user.Password));
                    if (isValid)
                    {
                        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, user.Email) },
                            CookieAuthenticationDefaults.AuthenticationScheme);
                        identity.AddClaim(new Claim(ClaimTypes.Name, userdata.FirstName));
                        identity.AddClaim(new Claim(ClaimTypes.Surname, userdata.LastName));
                        var principle = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);
                        HttpContext.Session.SetString("Email", user.Email);
                        if (ReturnUrl != null)
                        {
                            var viewurl = ReturnUrl.Split('/');
                            return RedirectToAction(viewurl[3], viewurl[2], new { Area = viewurl[1] });
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home", new { Area = "Volunteer" });
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
            else
            {
                ViewBag.error = "Email Id not Found!";
            }
            return View();
        }
        public IActionResult ForgotPass()
        {
            if (TempData["error"] != null)
                ViewBag.error = TempData["error"];
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPass(string email)
        {
            var data = _db.Users.Where(x => x.Email == email).SingleOrDefault();
            if (data != null)
            {
                var dataToken = _db.UserTokens.Where(x => x.Email == email).SingleOrDefault();
                string token = TokenGenrate();
                UserToken emailtoken = new UserToken();
                if (dataToken != null)
                {
                    dataToken.UserToken1 = token;
                    dataToken.Used = 0;
                    dataToken.GeneratedAt = DateTime.Now;
                }
                else
                {
                    emailtoken.Email = email;
                    emailtoken.UserToken1 = token;
                    emailtoken.Used = 0;
                    emailtoken.GeneratedAt = DateTime.Now;
                    _db.UserTokens.Add(emailtoken);
                }
                _db.SaveChanges();
                var link = Url.Action("ResetPass", "Login", new { Area = "Volunteer", email = email, token = token });
                var mailBody = "<h1>Reset Password Link:</h1><br> <a href='https://localhost:44365" + link + "'> <b style='color:red;'>Click Here to Forgot Password</b>  </a>";
                Email(mailBody, email);
            }
            else
            {
                ViewBag.error = "Email Not Found";
            }
            return View();
        }
        public IActionResult ResetPass(string email, string token)
        {
            var dataToken = _db.UserTokens.Where(x => x.Email == email).SingleOrDefault();
            if (dataToken != null)
            {
                if (dataToken.UserToken1 == token)
                {
                    ViewBag.email = email;
                    return View();
                }
            }
            TempData["error"] = "Something was changed in Url or Url was expired! Please try again!";
            return RedirectToAction("ForgotPass", "Login", new { Area = "Volunteer" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPass(FormCollection form)
        {
            var user = _db.Users.Where(x => x.Email == form["email"]).SingleOrDefault();
            if (user != null)
            {
                user.Password = form["pass"];
                user.UpdatedAt = DateTime.Now;
                _db.SaveChanges();
            }
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(User user)
        {
            if (_db.Users.Where(x => x.Email == user.Email).SingleOrDefault() == null)
            {
                _db.Users.Add(user);
                _db.SaveChanges();
            }
            else
            {
                ViewBag.error = user.Email + " was already Registered!";
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home", new { Area = "Volunteer" });
        }


        //for sending email
        public static void Email(string body, string mailid)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("ciplatform123@gmail.com");
                message.To.Add(new MailAddress(mailid));
                message.Subject = "Your Password For CI-Platform";
                message.IsBodyHtml = true;
                message.Body = body;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("ciplatform123@gmail.com", "qdkcganrxcfzyaqh");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
        }


        public static string TokenGenrate()
        {
            string[] str = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "_", "*", "$", "!" };
            string token = "";
            Random r = new Random();
            for (int i = 0; i < 20; i++)
            {
                token += str[r.Next(0, str.Length)];
            }
            return token;
        }
    }
}