using Microsoft.AspNetCore.Mvc;
using TripManager.Infrastructure.OperationResponse;

namespace TripManager.API.Controllers
{
    [ApiVersion("1")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {

        public IActionResult Return<T>(OperationResponse<T> response)
        {
            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest();
        }
    }
}
