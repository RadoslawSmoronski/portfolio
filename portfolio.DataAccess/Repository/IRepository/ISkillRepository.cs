using portfolio.Models.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.DataAccess.Repository.IRepository
{
    public interface ISkillRepository : IRepository<Skill> 
    {
        void Update(Skill obj);
        List<SkillView> GetAllView(string languageCode);
    }
}
