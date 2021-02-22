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
    public class NdepoTypeController : ControllerBase
    {
        private readonly INdepoTypeService _service;
        public NdepoTypeController(INdepoTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<GridData<NdepoTypeDTO>>> GetNdepoType([FromQuery] string queryParams)
        {
            var qParams = JsonConvert.DeserializeObject<GridData<NdepoTypeDTO>>(queryParams);
            return Ok(await _service.Get(qParams));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NdepoTypeDTO>> GetNdepoType(long id)
        {
            return Ok(await _service.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNdepoType(long id, NdepoTypeDTO depoType)
        {
            if (id != depoType.Id)
            {
                return BadRequest();
            }

            await _service.Update(depoType);

            try
            {
                await _service.Save();
            }
            catch (DbUpdateConcurrencyException) when (!_service.NdepoTypeExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> CreateNdepoType(NdepoTypeDTO depoType)
        {
            _service.Create(depoType);
            await _service.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNdepoType(long id)
        {
            await _service.Delete(id);
            await _service.Save();
            return NoContent();
        }
    }
}