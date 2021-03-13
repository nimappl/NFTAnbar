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
    public class KhzemanatNamehTypeController : ControllerBase
    {
        private readonly IKhzemanatNamehTypeService _service;
        public KhzemanatNamehTypeController(IKhzemanatNamehTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<GridData<KhzemanatNamehTypeDTO>>> GetKhzemanatNamehType([FromQuery] string queryParams)
        {
            var qParams = JsonConvert.DeserializeObject<GridData<KhzemanatNamehTypeDTO>>(queryParams);
            return Ok(await _service.Get(qParams));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KhzemanatNamehTypeDTO>> GetKhzemanatNamehType(long id)
        {
            if (!_service.KhzemanatNamehTypeExists(id))
                return NotFound();

            return Ok(await _service.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateKhzemanatNamehType(long id, KhzemanatNamehTypeDTO zemanatNamehType)
        {
            if (id != zemanatNamehType.Id)
            {
                return BadRequest();
            }

            await _service.Update(zemanatNamehType);

            try
            {
                await _service.Save();
            }
            catch (DbUpdateConcurrencyException) when (!_service.KhzemanatNamehTypeExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> CreateKhzemanatNamehType(KhzemanatNamehTypeDTO dto)
        {
            _service.Create(dto);
            await _service.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteKhzemanatNamehType(long id)
        {
            await _service.Delete(id);
            await _service.Save();
            return NoContent();
        }
    }
}