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
        // TODO: Rozważyć wpięcie Middleware do konwertacji wyjątków na komunikaty
        try
        {
            await ResourceManagementProcessManager.AddResourceAsync(command.Id, command.Name, command.UserId, command.UserToken);
        }
        catch (AccessDenied ex)
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            //TODO Dodanie loggera
            return Problem("Wystąpił błąd podczas wykonywania akcji");
        }

        return Ok();
    }

    [HttpPost("CancelResource")]
    public async Task<ActionResult> CancelResource([FromBody] CancelResource command)
    {

        try
        {
            await ResourceManagementProcessManager.CancelResourceAsync(command.ResourceId, command.UserId, command.UserToken);
        }
        catch (AccessDenied ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ResourceNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            return Problem("Wystąpił błąd podczas wykonywania akcji");
        }

        return Ok();
    }
}
