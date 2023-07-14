using Microsoft.AspNetCore.Mvc;
using TripManager.Infrastructure.Dtos;
using TripManager.Infrastructure.Services;

namespace TripManager.API.Controllers
{
    public class VehicleTypeController : ApiBaseController
    {
        private readonly VehicleTypeService _service;

        public VehicleTypeController(VehicleTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> ListAll()
        {
            return Return(await _service.ListAll().ConfigureAwait(false));
        }
    }
}
