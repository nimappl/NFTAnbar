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
    public class SendTypeController : ControllerBase
    {
        private readonly ISendTypeService _service;
        public SendTypeController(ISendTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<GridData<SendTypeDTO>>> GetSendType([FromQuery] string queryParams)
        {
            var qParams = JsonConvert.DeserializeObject<GridData<SendTypeDTO>>(queryParams);
            return Ok(await _service.Get(qParams));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SendTypeDTO>> GetSendType(long id)
        {
            if (!_service.SendTypeExists(id))
                return NotFound();

            return Ok(await _service.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSendType(long id, SendTypeDTO sendType)
        {
            if (id != sendType.Id)
            {
                return BadRequest();
            }

            await _service.Update(sendType);

            try
            {
                await _service.Save();
            }
            catch (DbUpdateConcurrencyException) when (!_service.SendTypeExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> CreateSendType(SendTypeDTO dto)
        {
            _service.Create(dto);
            await _service.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSendType(long id)
        {
            await _service.Delete(id);
            await _service.Save();
            return NoContent();
        }
    }
}