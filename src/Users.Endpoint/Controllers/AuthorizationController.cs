using Microsoft.AspNetCore.Mvc;
using Users.Endpoint.Domain.Services;
using Users.Endpoint.InputModels;

namespace Users.Endpoint.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorizationController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthorizationController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("AuthorizeUser")]
    public ActionResult<bool> AuthorizeUser([FromBody] AuthorizeUserInput authorizeUserInput)
    {
        return Ok(_userService.Authorize(authorizeUserInput.UserId, authorizeUserInput.UserToken, authorizeUserInput.RequestedPermission));
    }
}
