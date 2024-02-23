using Toasted.Logic; 

namespace Toasted.Data
{
    public interface IRepository
    {
        puplic Task<IEnumerable<User>> GetAllUsersAsync();
        public Task<User> GetUserByUsernameAsync(int username);

        public Task AddUserAsync(User user);
    }
}
