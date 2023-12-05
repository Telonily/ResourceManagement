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
    public IActionResult AddResource([FromBody] AddResource command)
    {
        // TODO: Rozważyć wpięcie Middleware do konwertacji wyjątków na komunikaty
        try
        {
            ResourceManagementProcessManager.AddResource(command.Id, command.Name, command.UserId, command.UserToken);
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
    public IActionResult CancelResource([FromBody] CancelResource command)
    {

        try
        {
            ResourceManagementProcessManager.CancelResource(command.ResourceId, command.UserId, command.UserToken);
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
