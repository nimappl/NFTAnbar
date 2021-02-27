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
    public class PermitTypeController : ControllerBase
    {
        private readonly IPermitTypeService _service;
        public PermitTypeController(IPermitTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<GridData<PermitTypeDTO>>> GetPermitType([FromQuery] string queryParams)
        {
            var qParams = JsonConvert.DeserializeObject<GridData<PermitTypeDTO>>(queryParams);
            return Ok(await _service.Get(qParams));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PermitTypeDTO>> GetPermitType(long id)
        {
            if (!_service.PermitTypeExists(id))
                return NotFound();

            return Ok(await _service.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePermitType(long id, PermitTypeDTO permitType)
        {
            if (id != permitType.Id)
            {
                return BadRequest();
            }

            await _service.Update(permitType);

            try
            {
                await _service.Save();
            }
            catch (DbUpdateConcurrencyException) when (!_service.PermitTypeExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> CreatePermitType(PermitTypeDTO dto)
        {
            _service.Create(dto);
            await _service.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePermitType(long id)
        {
            await _service.Delete(id);
            await _service.Save();
            return NoContent();
        }
    }
}