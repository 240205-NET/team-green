﻿

namespace Toasted.Data
{
    public interface IRepository
    {
        public Task<IEnumerable<User>> GetAllUsersAsync();
        public Task<User> GetUserByUsernameAsync(string username);

        public Task<bool> AddUserAsync(User user);
    }
}
