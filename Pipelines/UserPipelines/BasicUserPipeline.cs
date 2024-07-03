using Workshop.Models;
using Workshop.Services;
using Workshop.Steps.CreateUserSteps;

namespace Workshop.Pipelines.UserPipelines
{
    public class BasicUserPipeline : PipelineBaseService<User>
    {
        private readonly IUserRepository _userRepository;

        public BasicUserPipeline(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected override List<IPipelineStep<User>> GetSteps()
        {
            return
            [
                new AddCurrencyAccountStep(new CurrencyAccount
                {
                    Currency = "EUR"
                }),
                new CreateInRepositoryStep(_userRepository),
            ];
        }
    }
}
