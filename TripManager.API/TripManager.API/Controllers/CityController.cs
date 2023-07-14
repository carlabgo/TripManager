using Microsoft.AspNetCore.Mvc;
using TripManager.Infrastructure.Dtos;
using TripManager.Infrastructure.Services;

namespace TripManager.API.Controllers
{
    public class CityController : ApiBaseController
    {
        private readonly CityService _service;

        public CityController(CityService service)
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
        public async Task<IActionResult> CreateOrUpdate([FromBody] DtoAddCity city)
        {
            return Return(await _service.CreateOrUpdateCity(city).ConfigureAwait(false));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            return Return(await _service.Delete(id).ConfigureAwait(false));
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> ListAll()
        {
            return Return(await _service.ListAll().ConfigureAwait(false));
        }
    }
}
