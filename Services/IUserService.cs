using Workshop.Models;

namespace Workshop.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Create(CreateUserRequest request)
        {
            var user = new User
            {
                CurrencyAccounts = new List<CurrencyAccount>()
                {
                    new CurrencyAccount
                    {
                        Currency = "EUR"
                    },
                },
                Name = request.Name,
                Type = request.Type,
            };

            await _userRepository.Create(user);

            return user;
        }
    }
}
