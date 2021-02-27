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
    public class HavalehController : ControllerBase
    {
        private readonly IHavalehService _service;
        public HavalehController(IHavalehService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<GridData<HavalehDTO>>> GetHavaleh([FromQuery] string queryParams)
        {
            var qParams = JsonConvert.DeserializeObject<GridData<HavalehDTO>>(queryParams);
            return Ok(await _service.Get(qParams));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HavalehDTO>> GetHavaleh(long id)
        {
            if (!_service.HavalehExists(id))
                return NotFound();

            return Ok(await _service.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateHavaleh(long id, HavalehDTO havaleh)
        {
            if (id != havaleh.Id)
            {
                return BadRequest();
            }

            await _service.Update(havaleh);

            try
            {
                await _service.Save();
            }
            catch (DbUpdateConcurrencyException) when (!_service.HavalehExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> CreateHavaleh(HavalehDTO dto)
        {
            _service.Create(dto);
            await _service.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHavaleh(long id)
        {
            await _service.Delete(id);
            await _service.Save();
            return NoContent();
        }
    }
}