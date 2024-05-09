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
        IProjectRepository ProjectRepository { get; }
        IContactRepository ContactRepository { get; }
        IEmailMessageRepository EmailMessageRepository { get; }

        void Save();
    }
}
