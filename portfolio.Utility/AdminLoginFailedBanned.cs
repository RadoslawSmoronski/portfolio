using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace portfolio.Utility
{
    public static class AdminLoginFailedBanned
    {
        static private Dictionary<string, int> failedLoginAttempts = new Dictionary<string, int>();

        static private List<string> bannedIps = new List<string>();

        public static void AddFailedLoginAttempts(string ip)
        {
            if(!failedLoginAttempts.ContainsKey(ip))
            {
                failedLoginAttempts[ip] = 0;
            }

            failedLoginAttempts[ip]++;


            if (failedLoginAttempts[ip] >= 4)
            {
                BanUser(ip);
            }
        }

        public static void RemoveFailderLoginAttempts(string ip)
        {
            failedLoginAttempts.Remove(ip);
        }

        public static bool IsUserBanned(string ip)
        {
            return bannedIps.Contains(ip);
        }

        private static void BanUser(string ipAddress)
        {
            if (!bannedIps.Contains(ipAddress))
            {
                Timer timer = new Timer(state =>
                {
                    failedLoginAttempts.Remove(ipAddress);
                    bannedIps.Remove(ipAddress);
                }, null, 5 * 60 * 1000, Timeout.Infinite);

                bannedIps.Add(ipAddress);
            }
        }
    }
}
