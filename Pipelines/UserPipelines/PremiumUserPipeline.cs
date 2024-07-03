using Workshop.Models;
using Workshop.Services;
using Workshop.Steps.CreateUserSteps;

namespace Workshop.Pipelines.UserPipelines
{
    public class PremiumUserPipeline : PipelineBaseService<User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public PremiumUserPipeline(
            IUserRepository userRepository,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        protected override List<IPipelineStep<User>> GetSteps()
        {
            return
            [
                new AddCurrencyAccountStep(new CurrencyAccount
                {
                    Currency = "EUR",
                    Amount = 5
                }),
                new CreateInRepositoryStep(_userRepository),
                new SendWelcomeEmailStep(_emailService),
            ];
        }
    }
}
