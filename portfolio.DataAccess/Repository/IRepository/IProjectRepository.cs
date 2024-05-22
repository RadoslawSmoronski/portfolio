using portfolio.Models.Project;
using portfolio.Models.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.DataAccess.Repository.IRepository
{
    public interface IProjectRepository : IRepository<Project>
    {
        void Update(Project obj);
        List<ProjectView> GetAllView(string languageCode);
    }
}
