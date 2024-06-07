using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.Utility
{
    public interface IAdminLoginFailedBanned
    {
        IMemoryCache Cache { get; }

        void AddFailedLoginAttempt(string ip);

        void RemoveFailedLoginAttempt(string ip);

        bool IsUserBanned(string ip);
    }
}
