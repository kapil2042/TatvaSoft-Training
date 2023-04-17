using CI_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repositories.Interfaces
{
    public interface IAdminMissionSkillsRepository
    {
        List<Skill> GetSkills(string query, int recSkip, int recTake);

        int GetTotalSkillsRecord(string query);

        void InsertSkill(Skill skill);

        void UpdateSkill(Skill skill);

        void DeleteSkill(Skill skill);

        Skill GetSkillById(long id);

        void DeleteMissionSkillDependency(long skillId);
    }
}
