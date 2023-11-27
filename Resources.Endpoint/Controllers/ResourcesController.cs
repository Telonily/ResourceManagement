using Microsoft.AspNetCore.Mvc;
using Resources.Endpoint.InputModels;
using Resources.Endpoint.Resources.Domain.Models.Exceptions;
using Resources.Endpoint.Resources.Domain.Services;

namespace Resources.Endpoint.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourcesController : ControllerBase
    {
        private readonly IResourceManagementService ResourceManagementService;

        public ResourcesController(IResourceManagementService resourceManagementService)
        {
            ResourceManagementService = resourceManagementService;
        }

        [HttpPost("AddResource")]
        public IActionResult AddResource([FromBody] AddResourceInput input)
        {
            ResourceManagementService.AddResource(input.Id, input.Name);
            return Ok();
        }

        [HttpPost("CancelResource")]
        public IActionResult CancelResource([FromBody] CancelResourceInput input)
        {
            // TODO: Rozważyć wpięcie Middleware do konwertacji wyjątków na komunikaty
            try
            {
                ResourceManagementService.CancelResource(input.ResourceId, input.CancelerUserId);
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
