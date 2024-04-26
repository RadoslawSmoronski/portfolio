using portfolio.DataAccess.Data;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models;
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

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Skill obj)
        {
            _db.Skills.Update(obj);
        }
    }
}
