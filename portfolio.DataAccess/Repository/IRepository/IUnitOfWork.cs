using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ISkillRepository SkillRepository { get; }

        void Save();
    }
}
