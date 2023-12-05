using Microsoft.AspNetCore.Mvc;
using PublishedLanguage;
using Resources.Application.Services;
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
    public IActionResult LockResourceTemporary(LockResourceTemporarily command)
    {
        try
        {
            ResourceBlockadeProcessManager.LockResourceTemporary(command.ResourceId, command.UserId, command.UserToken);
            return Ok();
        }
        catch (BusinessException ex) { return BadRequest(ex.Message); }
        catch (Exception ex) { return Problem(ex.Message); }
    }


    [HttpPost("LockResourcePermanently")]
    public IActionResult LockResourcePermanently(LockResourcePermanently command)
    {
        try
        {
            ResourceBlockadeProcessManager.LockResourcePermanently(command.ResourceId, command.UserId, command.UserToken);
            return Ok();
        }
        catch (BusinessException ex) { return BadRequest(ex.Message); }
        catch (Exception ex) { return Problem(ex.Message); }
    }

    [HttpPost("ReleaseResource")]
    public IActionResult ReleaseResource(ReleaseResource command)
    {
        try
        {
            ResourceBlockadeProcessManager.ReleaseResource(command.ResourceId, command.UserId, command.UserToken);
            return Ok();
        }
        catch (BusinessException ex) { return BadRequest(ex.Message); }
        catch (Exception ex) { return Problem(ex.Message); }
    }
}
