using FluentAssertions;
using Moq;
using Workshop.Models;
using Workshop.Services;

namespace Workshop.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _repositoryMock;
        private readonly Mock<IEmailService> _mailServiceMock;
        private readonly UserService _sut;

        public UserServiceTests()
        {
            _repositoryMock = new Mock<IUserRepository>();
            _mailServiceMock = new Mock<IEmailService>();
            _sut = new UserService(_repositoryMock.Object, _mailServiceMock.Object);
        }

        [Theory]
        [InlineData(UserType.Basic)]
        [InlineData(UserType.Premium)]
        [InlineData(UserType.VIP)]
        public async Task Create_WhenInvoked_ThenCreateUserWithEurAccount(UserType type)
        {
            var request = new CreateUserRequest
            {
                Name = Guid.NewGuid().ToString(),
                Type = type,
            };

            var result = await _sut.Create(request);

            result.CurrencyAccounts.First().Currency.Should().Be("EUR");
        }

        [Theory]
        [InlineData(UserType.Basic)]
        [InlineData(UserType.Premium)]
        [InlineData(UserType.VIP)]
        public async Task Create_WhenInvoked_ThenRepositoryCreateUser(UserType type)
        {
            var request = new CreateUserRequest
            {
                Name = Guid.NewGuid().ToString(),
                Type = type,
            };

            await _sut.Create(request);

            _repositoryMock.Verify(x => x.Create(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task Create_WhenTypeIsPremium_ThenCreateUserWith5Eur()
        {
            var request = new CreateUserRequest
            {
                Name = Guid.NewGuid().ToString(),
                Type = UserType.Premium,
            };

            var result = await _sut.Create(request);

            result.CurrencyAccounts.First(x => x.Currency == "EUR").Amount.Should().Be(5);
        }

        [Fact]
        public async Task Create_WhenTypeIsVIP_ThenCreateUserWith50Eur()
        {
            var request = new CreateUserRequest
            {
                Name = Guid.NewGuid().ToString(),
                Type = UserType.VIP,
            };

            var result = await _sut.Create(request);

            result.CurrencyAccounts.First(x => x.Currency == "EUR").Amount.Should().Be(50);
        }

        [Fact]
        public async Task Create_WhenTypeIsBasic_ThenCreateUserWith0Eur()
        {
            var request = new CreateUserRequest
            {
                Name = Guid.NewGuid().ToString(),
                Type = UserType.Basic,
            };

            var result = await _sut.Create(request);

            result.CurrencyAccounts.First(x => x.Currency == "EUR").Amount.Should().Be(0);
        }

        [Fact]
        public async Task Create_WhenTypeIsVIP_ThenCreateUserWithUsdAccount()
        {
            var request = new CreateUserRequest
            {
                Name = Guid.NewGuid().ToString(),
                Type = UserType.VIP,
            };

            var result = await _sut.Create(request);

            result.CurrencyAccounts.Should().Contain(x => x.Currency == "USD");
        }

        [Theory]
        [InlineData(UserType.Premium)]
        [InlineData(UserType.VIP)]
        public async Task Create_WhenTypeIsPremiumOrVIP_ThenSendWelcomeEmail(UserType type)
        {
            var request = new CreateUserRequest
            {
                Name = Guid.NewGuid().ToString(),
                Type = type,
            };

            await _sut.Create(request);

            _mailServiceMock.Verify(x => x.SendWelcomeEmail(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task Create_WhenTypeIsBasic_ThenDoNotSendWelcomeEmail()
        {
            var request = new CreateUserRequest
            {
                Name = Guid.NewGuid().ToString(),
                Type = UserType.Basic,
            };

            await _sut.Create(request);

            _mailServiceMock.Verify(x => x.SendWelcomeEmail(It.IsAny<User>()), Times.Never);
        }
    }
}