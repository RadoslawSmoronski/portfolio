using portfolio.DataAccess.Data;
using portfolio.DataAccess.Repository.IRepository;
using portfolio.Models;
using portfolio.Models.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.DataAccess.Repository
{
    public class EmailMessageRepository : Repository<EmailMessage>, IEmailMessageRepository
    {
        private ApplicationDbContext _db;

        public EmailMessageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public int GetUnreadAmount()
        {
            IQueryable<EmailMessage> query = dbSet;

            int i = 0;

            foreach (EmailMessage message in query)
            {
                if(message.IsReaded == false)
                {
                    i++;
                }
            }

            return i;
        }

        public void Update(EmailMessage obj)
        {
            _db.EmailMessages.Update(obj);
        }
    }
}
