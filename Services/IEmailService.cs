using Workshop.Models;

namespace Workshop.Services
{
    public interface IEmailService
    {
        Task SendWelcomeEmail(User user);
    }
}
