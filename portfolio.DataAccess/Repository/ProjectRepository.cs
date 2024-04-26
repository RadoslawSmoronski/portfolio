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
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private ApplicationDbContext _db;

        public ProjectRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Project obj)
        {
            _db.Projects.Update(obj);
        }
    }
}
