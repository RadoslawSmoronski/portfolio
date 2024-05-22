using portfolio.DataAccess.Data;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models.Project;
using portfolio.Models.Skill;
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

        public List<ProjectView> GetAllView(string languageCode)
        {
            List<ProjectView> viewList = new List<ProjectView>();
            List<Project> objList = GetAll().ToList();

            foreach (Project obj in objList)
            {
                ProjectView viewObj = new ProjectView(obj, languageCode);
                viewList.Add(viewObj);
            }

            return viewList;
        }
    }
}
