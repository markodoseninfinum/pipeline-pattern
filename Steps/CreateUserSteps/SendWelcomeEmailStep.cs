using Workshop.Models;
using Workshop.Pipelines;
using Workshop.Services;

namespace Workshop.Steps.CreateUserSteps
{
    public class SendWelcomeEmailStep : IPipelineStep<User>
    {
        private readonly IEmailService _emailService;

        public SendWelcomeEmailStep(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Execute(User context)
        {
            await _emailService.SendWelcomeEmail(context);
        }
    }
}
