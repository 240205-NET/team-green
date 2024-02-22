using Toasted.Logic; 

namespace Toasted.Data
{
    public interface IRepository
    {
        puplic Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
