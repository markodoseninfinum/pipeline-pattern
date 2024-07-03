using Workshop.Models;

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
                user.CurrencyAccounts.Add(new CurrencyAccount
                {
                    Currency = "USD"
                });
            }

            await _userRepository.Create(user);

            if (request.Type == UserType.VIP || request.Type == UserType.Premium)
            {
                await _emailService.SendWelcomeEmail(user);
            }

            return user;
        }
    }
}
