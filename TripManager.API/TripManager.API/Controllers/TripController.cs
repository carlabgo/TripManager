using Microsoft.AspNetCore.Mvc;
using TripManager.Infrastructure.Dtos;
using TripManager.Infrastructure.Dtos.Trip;
using TripManager.Infrastructure.Services;

namespace TripManager.API.Controllers
{
    public class TripController : ApiBaseController
    {
        private readonly TripService _service;

        public TripController(TripService service)
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
        public async Task<IActionResult> CreateOrUpdate([FromBody] DtoAddTrip trip)
        {
            return Return(await _service.CreateOrUpdateTrip(trip).ConfigureAwait(false));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            return Return(await _service.Delete(id).ConfigureAwait(false));
        }

        [HttpPost]
        [Route("list")]
        public async Task<IActionResult> List([FromBody] DtoFilterTrip trip)
        {
            return Return(await _service.ListTrips(trip).ConfigureAwait(false));
        }
    }
}
