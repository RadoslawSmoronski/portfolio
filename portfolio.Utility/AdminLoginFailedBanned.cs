using Microsoft.Extensions.Caching.Memory;

namespace portfolio.Utility
{
    public class AdminLoginFailedBanned : IAdminLoginFailedBanned
    {
        public IMemoryCache Cache { get; set; }

        public AdminLoginFailedBanned(IMemoryCache memoryCache)
        {
            Cache = memoryCache;
        }

        public void AddFailedLoginAttempt(string ip)
        {
            var attempts = Cache.GetOrCreate("FailedLoginAttempts", entry => new Dictionary<string, int>());

            if (attempts == null) return;

            if (!attempts.ContainsKey(ip))
            {
                attempts[ip] = 0;
            }

            attempts[ip]++;

            if (attempts[ip] >= 4)
            {
                BanUser(ip);
            }
        }

        public void RemoveFailedLoginAttempt(string ip)
        {
            var attempts = Cache.GetOrCreate("FailedLoginAttempts", entry => new Dictionary<string, int>());
            if (attempts == null) return;
            attempts.Remove(ip);
        }

        public bool IsUserBanned(string ip)
        {
            var bannedIps = Cache.GetOrCreate("BannedIps", entry => new HashSet<string>());
            if (bannedIps == null) return false;
            return bannedIps.Contains(ip);
        }

        private void BanUser(string ipAddress)
        {
            var bannedIps = Cache.GetOrCreate("BannedIps", entry => new HashSet<string>());
            if (bannedIps == null) return;
            if (!bannedIps.Contains(ipAddress))
            {
                bannedIps.Add(ipAddress);
                Cache.Set("BannedIps", bannedIps);

                Cache.Remove("FailedLoginAttempts");
            }
        }
    }
}
