using Workshop.Models;
using Workshop.Pipelines;
using Workshop.Services;

namespace Workshop.Steps.CreateUserSteps
{
    public class CreateInRepositoryStep : IPipelineStep<User>
    {
        private readonly IUserRepository _userRepository;

        public CreateInRepositoryStep(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Execute(User context)
        {
            await _userRepository.Create(context);
        }
    }
}
