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

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetUsers")]
        public bool Get()
        {
            return true;
        }

        [HttpPost("/UserCheck")]
        public bool Post([FromBody] string username)
        {
            //check database if exists return true

            SqlRepository sqlRepository = new SqlRepository(_logger);
          User user =  sqlRepository.GetUserByUsernameAsync(username);

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
