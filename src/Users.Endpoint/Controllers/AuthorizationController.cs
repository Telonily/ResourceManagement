using Microsoft.AspNetCore.Mvc;
using Users.Endpoint.Domain.Services;
using Users.Endpoint.InputModels;

namespace Users.Endpoint.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserService UserService;

        public AuthorizationController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpPost("AuthorizeUser")]
        public ActionResult<bool> AuthorizeUser([FromBody] AuthorizeUserInput authorizeUserInput)
        {
            return Ok(UserService.Authorize(authorizeUserInput.UserLogin, authorizeUserInput.UserToken, authorizeUserInput.RequestedPermission));
        }
    }
}
