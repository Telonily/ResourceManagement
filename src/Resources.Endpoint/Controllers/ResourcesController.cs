using Microsoft.AspNetCore.Mvc;
using Resources.Application.Exceptions;
using Resources.Application.Services;
using Resources.Endpoint.Commands;

namespace Resources.Endpoint.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResourcesController : ControllerBase
{
    private readonly IResourceManagementProcessManager ResourceManagementProcessManager;

    public ResourcesController(IResourceManagementProcessManager resourceManagementProcessManager)
    {
        ResourceManagementProcessManager = resourceManagementProcessManager;
    }

    [HttpPost("AddResource")]
    public async Task<ActionResult> AddResource([FromBody] AddResource command)
    {
        await ResourceManagementProcessManager.AddResourceAsync(command.Id, command.Name, command.UserId, command.UserToken);
        return Ok();
    }

    [HttpPost("CancelResource")]
    public async Task<ActionResult> CancelResource([FromBody] CancelResource command)
    {
        await ResourceManagementProcessManager.CancelResourceAsync(command.ResourceId, command.UserId, command.UserToken);
        return Ok();
    }
}
