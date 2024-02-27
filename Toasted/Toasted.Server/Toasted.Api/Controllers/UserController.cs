using Microsoft.AspNetCore.Mvc;
using Toasted.Api;
using Toasted.Data;

namespace Toasted.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IRepository _repo;

        public UserController(IRepository repo, ILogger<UserController> logger)
        {
            this._logger = logger;
            this._repo = repo;
        }

        [HttpPost("/GetUserByUsername")] //returns USER by username
        public async Task<User> GetUserInformation([FromBody] string username)
        {
            User user = await _repo.GetUserByUsernameAsync(username);
            _logger.LogInformation($"Username: {user.username}, Temperature Preference is: {user.tempUnit}");
            return user;
       
        }



        [HttpPost("/AuthenticateUser")]
        public async Task<bool> PostAuthenticate([FromBody] string[] userPass)
        {
            string username = userPass[0];
            string encryptedPassword = userPass[1];
          


            User user = await _repo.GetUserByUsernameAsync(username);
             _logger.LogInformation($"Username: {user.username}, Temperature Preference is: {user.tempUnit}" );

            if (user.password == encryptedPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }




        [HttpPost("/UserCheck")]
        public async Task<bool> Post([FromBody] string username)
        {
            //check database if exists return true

            User user = await _repo.GetUserByUsernameAsync(username);

            if (user.userId == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        [HttpPost("/NewAccount")]
        public async Task<bool> PostNewUser([FromBody] User user)
        {
            _logger.LogInformation($"Username: {user.username}, Temperature Preference is: {user.tempUnit}" );

            bool result = await _repo.AddUserAsync(user);

            //in progress


            _logger.LogInformation($"{user.username}, {user.firstName} {user.lastName}, Location: {user.location.ToString()}");
         //   throw new NotImplementedException();
            return result;
        }
        








    }
}
