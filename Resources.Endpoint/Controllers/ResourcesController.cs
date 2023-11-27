using Microsoft.AspNetCore.Mvc;
using Resources.Endpoint.InputModels;
using Resources.Endpoint.Models.Exceptions;
using Resources.Endpoint.ProcessManagers;
using Resources.Endpoint.Resources.Domain.Models.Exceptions;
using Resources.Endpoint.Resources.Domain.Services;

namespace Resources.Endpoint.Controllers
{
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
        public IActionResult AddResource([FromBody] AddResourceInput input)
        {
            // TODO: Rozważyć wpięcie Middleware do konwertacji wyjątków na komunikaty
            try
            {
                ResourceManagementProcessManager.AddResource(input.Id, input.Name, input.UserId, input.UserToken);
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
        public IActionResult CancelResource([FromBody] CancelResourceInput input)
        {
            
            try
            {
                ResourceManagementProcessManager.CancelResource(input.ResourceId, input.UserId, input.UserToken);
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
}
