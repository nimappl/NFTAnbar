using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NFTAnbarAPI.DTOs;
using NFTAnbarAPI.Services;

namespace NFTAnbarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermitController : ControllerBase
    {
        private readonly IPermitService _service;
        public PermitController(IPermitService service)
        {
            _service = service;

        }

        [HttpGet]
        public async Task<ActionResult<GridData<PermitDTO>>> GetPermits([FromQuery] string queryParams)
        {
            var qParams = JsonConvert.DeserializeObject<GridData<PermitDTO>>(queryParams);
            return Ok(await _service.Get(qParams));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PermitDTO>> GetPermits(long id)
        {
            if (!_service.PermitExists(id))
                return NotFound();

            return Ok(await _service.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePermit(long id, PermitDTO permit)
        {
            if (id != permit.Id)
            {
                return BadRequest();
            }

            await _service.Update(permit);

            try
            {
                await _service.Save();
            }
            catch (DbUpdateConcurrencyException) when (!_service.PermitExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> CreatePermit(PermitDTO permit)
        {
            _service.Create(permit);
            await _service.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePermit(long id)
        {
            await _service.Delete(id);
            await _service.Save();
            return NoContent();
        }
    }
}