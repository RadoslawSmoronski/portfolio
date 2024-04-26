using portfolio.DataAccess.Data;
using portfolio.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ISkillRepository SkillRepository { get; private set; }
        public IProjectRepository ProjectRepository { get; private set; }

        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            SkillRepository = new SkillRepository(_db);
            ProjectRepository = new ProjectRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
