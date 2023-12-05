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
        try
        {
            await ResourceBlockadeProcessManager.LockResourceTemporaryAsync(command.ResourceId, command.UserId, command.UserToken);
            return Ok();
        }
        catch (BusinessException ex) { return BadRequest(ex.Message); }
        catch (Exception ex) { return Problem(ex.Message); }
    }


    [HttpPost("LockResourcePermanently")]
    public async Task<ActionResult> LockResourcePermanently(LockResourcePermanently command)
    {
        try
        {
            await ResourceBlockadeProcessManager.LockResourcePermanentlyAsync(command.ResourceId, command.UserId, command.UserToken);
            return Ok();
        }
        catch (BusinessException ex) { return BadRequest(ex.Message); }
        catch (Exception ex) { return Problem(ex.Message); }
    }

    [HttpPost("ReleaseResource")]
    public async Task<ActionResult> ReleaseResource(ReleaseResource command)
    {
        try
        {
            await ResourceBlockadeProcessManager.ReleaseResourceAsync(command.ResourceId, command.UserId, command.UserToken);
            return Ok();
        }
        catch (BusinessException ex) { return BadRequest(ex.Message); }
        catch (Exception ex) { return Problem(ex.Message); }
    }
}
