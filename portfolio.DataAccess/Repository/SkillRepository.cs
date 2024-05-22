using portfolio.DataAccess.Data;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.DataAccess.Repository
{
    public class SkillRepository : Repository<Skill>, ISkillRepository
    {
        private ApplicationDbContext _db;

        public SkillRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;    
        }

        public void Update(Skill obj)
        {
            _db.Skills.Update(obj);
        }

        public List<SkillView> GetAllView(string languageCode)
        {
            List<SkillView> viewList = new List<SkillView>();
            List<Skill> objList = GetAll().ToList();

            foreach (Skill obj in objList)
            {
                SkillView viewObj = new SkillView(obj, languageCode);
                viewList.Add(viewObj);
            }

            return viewList;
        }
    }
}
