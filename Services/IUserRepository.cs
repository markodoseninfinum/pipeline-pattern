using Workshop.Models;

namespace Workshop.Services
{
    public interface IUserRepository
    {
        public Task<User> Get(string name);
        public Task<User> Create(User user);
    }
}
