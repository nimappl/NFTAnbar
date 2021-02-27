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
    public class NdepoWorkShiftController : ControllerBase
    {
        private readonly INdepoWorkShiftService _service;
        public NdepoWorkShiftController(INdepoWorkShiftService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<GridData<NdepoWorkShiftDTO>>> GetNdepoWorkShift([FromQuery] string queryParams)
        {
            var qParams = JsonConvert.DeserializeObject<GridData<NdepoWorkShiftDTO>>(queryParams);
            return Ok(await _service.Get(qParams));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NdepoWorkShiftDTO>> GetNdepoWorkShift(long id)
        {
            if (!_service.NdepoWorkShiftExists(id))
                return NotFound();

            return Ok(await _service.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNdepoWorkShift(long id, NdepoWorkShiftDTO depoWorkShift)
        {
            if (id != depoWorkShift.Id)
            {
                return BadRequest();
            }

            await _service.Update(depoWorkShift);

            try
            {
                await _service.Save();
            }
            catch (DbUpdateConcurrencyException) when (!_service.NdepoWorkShiftExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> CreateNdepoWorkShift(NdepoWorkShiftDTO dto)
        {
            _service.Create(dto);
            await _service.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNdepoWorkShift(long id)
        {
            await _service.Delete(id);
            await _service.Save();
            return NoContent();
        }
    }
}