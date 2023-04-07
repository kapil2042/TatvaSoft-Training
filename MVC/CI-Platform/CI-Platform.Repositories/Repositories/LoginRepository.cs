using CI_Platform.Data;
using CI_Platform.Models;
using CI_Platform.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CI_Platform.Models.ViewModels;

namespace CI_Platform.Repositories.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly CiPlatformContext _db;
        private readonly ICommonRepository _c;

        public LoginRepository(CiPlatformContext db, ICommonRepository c)
        {
            _db = db;
            _c = c;
        }

        public UserToken getTokenByEmail(string email)
        {
            return _db.UserTokens.Where(x => x.Email == email).FirstOrDefault();
        }

        public User getUserByEmail(string email)
        {
            return _db.Users.Where(x => x.Email == email).FirstOrDefault();
        }

        public void InsertUser(VMUserRegistration user)
        {
            User u = new User();
            u.FirstName = user.FirstName;
            u.LastName = user.LastName;
            u.Email = user.Email;
            u.Password = _c.Encode(user.Password);
            u.PhoneNumber = user.MobileNo;
            _db.Users.Add(u);
        }

        public void InsertToken(UserToken token)
        {
            _db.UserTokens.Add(token);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void UpdateToken(UserToken token)
        {
            _db.UserTokens.Update(token);
        }

        public string TokenGenerate()
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

        public void SendMail(string sub, string body, string mailid)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("lrs.aau.in@gmail.com");
                message.To.Add(new MailAddress(mailid));
                message.Subject = sub;
                message.IsBodyHtml = true;
                message.Body = body;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("lrs.aau.in@gmail.com", "nlugxyghjtuxmmqj");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
        }
    }
}
