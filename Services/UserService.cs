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

            if (request.Type == UserType.Premium)
            {
                user.CurrencyAccounts.First(x => x.Currency == "EUR").Amount = 5;
            }

            if (request.Type == UserType.VIP)
            {
                user.CurrencyAccounts.First(x => x.Currency == "EUR").Amount = 50;
            }

            await _userRepository.Create(user);

            return user;
        }
    }
}
