using portfolio.DataAccess;
using portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.Utility.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string? subject, string? content);
    }
}
