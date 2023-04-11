using CI_Platform.Data;
using CI_Platform.Models;
using CI_Platform.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repositories.Repositories
{
    public class AdminMissionSkillRepository : IAdminMissionSkillsRepository
    {
        private readonly CiPlatformContext _db;

        public AdminMissionSkillRepository(CiPlatformContext db)
        {
            _db = db;
        }

        public List<Skill> GetSkills(string query, int recSkip, int recTake)
        {
            return _db.Skills.Where(x => x.SkillName.Contains(query)).Skip(recSkip).Take(recTake).ToList();
        }

        public int GetTotalSkillsRecord(string query)
        {
            return _db.Skills.Count(x => x.SkillName.Contains(query));
        }

        public void InsertSkill(Skill skill)
        {
            _db.Skills.Add(skill);
        }

        public void UpdateSkill(Skill skill)
        {
            _db.Skills.Update(skill);
        }

        public void DeleteSkill(Skill skill)
        {
            _db.Skills.Remove(skill);
        }

        public Skill GetSkillById(long id)
        {
            return _db.Skills.Where(x => x.SkillId == id).FirstOrDefault();
        }
    }
}
