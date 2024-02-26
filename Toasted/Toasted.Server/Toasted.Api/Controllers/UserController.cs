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

        [HttpGet(Name = "GetUsers")]
        public bool Get()
        {
            return true;
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

        








    }
}
