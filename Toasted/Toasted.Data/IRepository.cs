using Toasted.Logic; 

namespace Toasted.Data
{
    public interface IRepository
    {
        public Task<IEnumerable<User>> GetAllUsersAsync();
        public Task<User> GetUserByUsernameAsync(int username);

        public Task AddUserAsync(User user);
    }
}
