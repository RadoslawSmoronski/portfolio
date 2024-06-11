using Microsoft.Extensions.Caching.Memory;

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
