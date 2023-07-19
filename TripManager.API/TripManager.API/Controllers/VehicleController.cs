using Microsoft.AspNetCore.Mvc;
using TripManager.Infrastructure.Dtos;
using TripManager.Infrastructure.Dtos.Trip;
using TripManager.Infrastructure.Dtos.Vehicle;
using TripManager.Infrastructure.Services;

namespace TripManager.API.Controllers
{
    public class VehicleController : ApiBaseController
    {
        private readonly VehicleService _service;

        public VehicleController(VehicleService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Return(await _service.GetById(id).ConfigureAwait(false));
        }

        [HttpPost]
        [Route("createOrUpdate")]
        public async Task<IActionResult> CreateOrUpdate([FromBody] DtoAddVehicle vehicle)
        {
            return Return(await _service.CreateOrUpdateVehicle(vehicle).ConfigureAwait(false));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            return Return(await _service.Delete(id).ConfigureAwait(false));
        }

        [HttpGet]
        [Route("listAll")]
        public async Task<IActionResult> ListAll()
        {
            return Return(await _service.ListAll().ConfigureAwait(false));
        }

        [HttpPost]
        [Route("list")]
        public async Task<IActionResult> List([FromBody] DtoFilterVehicle vehicle)
        {
            return Return(await _service.ListVehicles(vehicle).ConfigureAwait(false));
        }
    }
}
