using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NFTAnbarAPI.DTOs;
using NFTAnbarAPI.Services;
using Newtonsoft.Json;

namespace NFTAnbarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _service;
        public CityController(ICityService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<GridData<CityDTO>>> GetCity([FromQuery] string queryParams)
        {
            var qParams = JsonConvert.DeserializeObject<GridData<CityDTO>>(queryParams);
            return Ok(await _service.Get(qParams));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityDTO>> GetCity(long id)
        {
            if (!_service.CityExists(id))
                return NotFound();

            return Ok(await _service.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCity(long id, CityDTO city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }

            await _service.Update(city);

            try
            {
                await _service.Save();
            }
            catch (DbUpdateConcurrencyException) when (!_service.CityExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> CreateCity(CityDTO dto)
        {
            _service.Create(dto);
            await _service.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCity(long id)
        {
            await _service.Delete(id);
            await _service.Save();
            return NoContent();
        }
    }
}