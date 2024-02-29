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

        [HttpPost("/api/User")] //returns USER by username
        public async Task<User> GetUserInformation([FromBody] string username)
        {
            User user = await _repo.GetUserByUsernameAsync(username);
            _logger.LogInformation($"Username: {user.username}, Temperature Preference is: {user.tempUnit}");
            return user;
       
        }



        [HttpPost("/api/Authentication")]
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




        [HttpPost("/api/ExistingUser")]
        public async Task<bool> PostGetUserExists([FromBody] string username)
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

        [HttpPost("/api/ExistingEmail")]
        public async Task<bool> PostGetEmailExists([FromBody] string email)
        {
            //check if email exists
            return await _repo.checkEmailExistsAsync(email);

        }



        [HttpPost("/api/Account")]
        public async Task<bool> PostNewUser([FromBody] User user)
        {
            _logger.LogInformation($"Username: {user.username}, Temperature Preference is: {user.tempUnit}" );

            bool result = await _repo.AddUserAsync(user);


            _logger.LogInformation($"{user.username}, {user.firstName} {user.lastName}, Location: {user.location.ToString()}");
         //   throw new NotImplementedException();
            return result;
        }

        [HttpPatch("/api/EncryptedPassword")]
        public async Task<bool> PatchUpdatePassword([FromBody] string[] patch)
        {
            _logger.LogInformation($"Username: {patch[0]}, encrypted password: {patch[1]}");
            bool result = await _repo.UpdatePasswordAsync(patch[0], patch[1]);
            return result;
        }

        [HttpPatch("/api/TempUnit")]
        public async Task<bool> PatchUpdateTempUnit([FromBody] string[] patch)
        {
            _logger.LogInformation($"Username: {patch[0]}, Temp Unit: {patch[1]}");
            bool result = await _repo.UpdateTempUnitAsync(patch[0], char.Parse(patch[1]));
            return result;
        }

        [HttpPatch("/api/Location")]
        public async Task<bool> PatchUpdateLocation([FromBody] LocationUpdateContainer locationUpdateContainer)
        {
            _logger.LogInformation("UPDATE LOCATION");
            _logger.LogInformation($"Username: {locationUpdateContainer.username}, LocationJSON: {locationUpdateContainer.location.JsonThis()}");
            bool result = await _repo.UpdateLocationAsync(locationUpdateContainer.username,locationUpdateContainer.location.JsonThis()); //send JSON string to database
            return result;

        }







    }
}
