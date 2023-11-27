using Microsoft.AspNetCore.Mvc;
using Resources.Endpoint.InputModels;
using Resources.Endpoint.ProcessManagers;

namespace Resources.Endpoint.Controllers
{
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
        public IActionResult LockResourceTemporary(LockResourceInput input)
        {
            try
            {
                ResourceBlockadeProcessManager.LockResourceTemporary(input.ResourceId, input.UserId, input.UserToken);
                return Ok();
            }
            catch (Availabaility.Domain.Models.Exceptions.BusinessException ex) { return BadRequest(ex.Message); }
            catch (Models.Exceptions.BusinessException ex) { return BadRequest(ex.Message); }
            catch (Resources.Domain.Models.Exceptions.BusinessException ex) { return BadRequest(ex.Message); }
            catch (Exception ex) { return Problem(ex.Message); }
        }


        [HttpPost("LockResourcePermanently")]
        public IActionResult LockResourcePermanently(LockResourceInput input)
        {
            try
            {
                ResourceBlockadeProcessManager.LockResourcePermanently(input.ResourceId, input.UserId, input.UserToken);
                return Ok();
            }
            catch (Availabaility.Domain.Models.Exceptions.BusinessException ex) { return BadRequest(ex.Message); }
            catch (Models.Exceptions.BusinessException ex) { return BadRequest(ex.Message); }
            catch (Resources.Domain.Models.Exceptions.BusinessException ex) { return BadRequest(ex.Message); }
            catch (Exception ex) { return Problem(ex.Message); }
        }

        [HttpPost("ReleaseResource")]
        public IActionResult ReleaseResource(LockResourceInput input)
        {
            try
            {
                ResourceBlockadeProcessManager.ReleaseResource(input.ResourceId, input.UserId, input.UserToken);
                return Ok();
            }
            catch (Availabaility.Domain.Models.Exceptions.BusinessException ex) { return BadRequest(ex.Message); }
            catch (Models.Exceptions.BusinessException ex) { return BadRequest(ex.Message); }
            catch (Resources.Domain.Models.Exceptions.BusinessException ex) { return BadRequest(ex.Message); }
            catch (Exception ex) { return Problem(ex.Message); }
        }
    }
}
