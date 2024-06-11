namespace portfolio.Utility.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string? subject, string? content);
    }
}
