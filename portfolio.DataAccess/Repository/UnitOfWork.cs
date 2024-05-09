using portfolio.DataAccess.Data;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models.Email;
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
        public IContactRepository ContactRepository { get; private set; }
        public IEmailMessageRepository EmailMessageRepository { get; private set; }

        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            SkillRepository = new SkillRepository(_db);
            ProjectRepository = new ProjectRepository(_db);
            ContactRepository = new ContactRepository(_db);
            EmailMessageRepository = new EmailMessageRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
