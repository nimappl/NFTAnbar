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
    public class NaftkeshController : ControllerBase
    {
        private readonly INaftkeshService _service;
        public NaftkeshController(INaftkeshService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<GridData<NaftkeshDTO>>> GetNaftkesh([FromQuery] string queryParams)
        {
            var qParams = JsonConvert.DeserializeObject<GridData<NaftkeshDTO>>(queryParams);
            return Ok(await _service.Get(qParams));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NaftkeshDTO>> GetNaftkesh(long id)
        {
            if (!_service.NaftkeshExists(id))
                return NotFound();

            return Ok(await _service.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNaftkesh(long id, NaftkeshDTO naftkesh)
        {
            if (id != naftkesh.Id)
            {
                return BadRequest();
            }

            await _service.Update(naftkesh);

            try
            {
                await _service.Save();
            }
            catch (DbUpdateConcurrencyException) when (!_service.NaftkeshExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> CreateNaftkesh(NaftkeshDTO dto)
        {
            _service.Create(dto);
            await _service.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNaftkesh(long id)
        {
            await _service.Delete(id);
            await _service.Save();
            return NoContent();
        }
    }
}