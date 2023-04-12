using CI_Platform.Data;
using CI_Platform.Models;
using CI_Platform.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repositories.Repositories
{
    public class CommonRepository : ICommonRepository
    {
        private readonly CiPlatformContext _db;

        public CommonRepository(CiPlatformContext db)
        {
            _db = db;
        }

        public Admin getAdminByEmail(string email)
        {
            return _db.Admins.Where(x => x.Email == email).FirstOrDefault();
        }

        public List<User> GetAllUsers()
        {
            return _db.Users.Where(x => x.Status == 1).ToList();
        }

        public User GetUserById(long id)
        {
            return _db.Users.Where(x => x.UserId == id && x.Status == 1).Include(x => x.UserSkills).ThenInclude(x => x.Skill).FirstOrDefault();
        }

        public List<Country> GetCountries()
        {
            return _db.Countries.ToList();
        }

        public List<City> GetCities()
        {
            return _db.Cities.ToList();
        }

        public List<City> GetCitiesBycountry(int country)
        {
            return _db.Cities.Where(x => x.CountryId == country).ToList();
        }

        public List<Skill> GetSkills()
        {
            return _db.Skills.Where(x => x.Status == 1).ToList();
        }

        public List<MissionTheme> GetMissionThemes()
        {
            return _db.MissionThemes.Where(x => x.Status == 1).ToList();
        }

        public void UpdateUser(User user)
        {
            _db.Users.Update(user);
        }

        public void UpdateAdmin(Admin admin)
        {
            _db.Admins.Update(admin);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void SendMails(string sub, string body, string[] mailids)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("lrs.aau.in@gmail.com");
                foreach (var mailid in mailids)
                {
                    message.To.Add(mailid);
                }
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

        public long[] GetMissionsIdBySkillName(string[] skill)
        {
            return _db.MissionSkills.Include(x => x.Skill).Where(x => skill.Contains(x.Skill.SkillName)).Select(x => x.MissionId).ToArray();
        }

        public long GetUserIdByEmail(string email)
        {
            return _db.Users.Where(x => x.Email.Equals(email)).Select(x => x.UserId).FirstOrDefault();
        }

        public List<Mission> GetMissionByUserApply(int id)
        {
            return _db.Missions.Where(x => (_db.MissionApplicatoins.Where(x => x.UserId == id).Select(x => x.MissionId).ToList()).Contains(x.MissionId)).ToList();
        }


        public string Encode(string text)
        {
            try
            {
                byte[] encData_byte = new byte[text.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(text);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Encode" + ex.Message);
            }
        }

        public string Decode(string encoded_text)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encoded_text);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public List<CmsPage> getAllPrivacy()
        {
            return _db.CmsPages.Where(x => x.Status == 1).ToList();
        }

        public List<Banner> GetBanners()
        {
            return _db.Banners.OrderBy(x => x.SortOrder).ToList();
        }
    }
}