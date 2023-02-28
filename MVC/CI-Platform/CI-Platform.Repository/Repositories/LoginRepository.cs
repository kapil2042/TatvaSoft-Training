using CI_Platform.Data;
using CI_Platform.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Repositories
{
    internal class LoginRepository : ILoginRepository
    {
        private readonly CiPlatformContext _db;

        public LoginRepository(CiPlatformContext db)
        {
            _db = db;
        }
        public void Email(string body, string mailid)
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
                smtp.Credentials = new NetworkCredential("ciplatform123@gmail.com", "otoskohgreaywwof");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
        }
    }
}
