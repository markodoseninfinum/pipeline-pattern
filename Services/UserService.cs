using Workshop.Models;
using Workshop.Pipelines;
using Workshop.Pipelines.UserPipelines;

namespace Workshop.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public UserService(
            IUserRepository userRepository,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task<User> Create(CreateUserRequest request)
        {
            var user = new User
            {
                Name = request.Name,
                Type = request.Type,
            };

            var pipeline = CreatePipeline(user.Type);

            await pipeline.Execute(user);

            return user;
        }

        private IPipelineService<User> CreatePipeline(UserType type)
        {
            switch (type)
            {
                case UserType.Basic:
                    return new BasicUserPipeline(_userRepository);
                case UserType.Premium:
                    return new PremiumUserPipeline(_userRepository, _emailService);
                case UserType.VIP:
                    return new VIPUserPipeline(_userRepository, _emailService);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
