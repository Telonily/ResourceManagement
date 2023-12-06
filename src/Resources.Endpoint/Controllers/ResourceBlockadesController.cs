using Microsoft.AspNetCore.Mvc;
using Resources.Application.Services;
using Resources.Core.Exceptions;
using Resources.Endpoint.Commands;

namespace Resources.Endpoint.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResourceBlockadesController : ControllerBase
{
    private readonly IResourceBlockadeProcessManager ResourceBlockadeProcessManager;

    public ResourceBlockadesController(IResourceBlockadeProcessManager resourceBlockadeProcessManager)
    {
        ResourceBlockadeProcessManager = resourceBlockadeProcessManager;
    }

    [HttpPost("LockResourceTemporary")]
    public async Task<ActionResult> LockResourceTemporary(LockResourceTemporarily command)
    {
        await ResourceBlockadeProcessManager.LockResourceTemporaryAsync(command.ResourceId, command.UserId, command.UserToken);
        return Ok();
    }


    [HttpPost("LockResourcePermanently")]
    public async Task<ActionResult> LockResourcePermanently(LockResourcePermanently command)
    {
        await ResourceBlockadeProcessManager.LockResourcePermanentlyAsync(command.ResourceId, command.UserId, command.UserToken);
        return Ok();
    }

    [HttpPost("ReleaseResource")]
    public async Task<ActionResult> ReleaseResource(ReleaseResource command)
    {
        await ResourceBlockadeProcessManager.ReleaseResourceAsync(command.ResourceId, command.UserId, command.UserToken);
        return Ok();
    }
}
