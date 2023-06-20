using File.Application.DTO.Request.User;
using File.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace File.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserApplication app;

        public UserController(IUserApplication app)
        {
            this.app = app;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateUser([FromBody] UserRegisterRequestDto user)
        {
            var response = await this.app.AddUser(user);
            return Ok(response);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginRequestDto user)
        {
            var response = await this.app.LoginUser(user);
            return Ok(response);
        }
    }
}
