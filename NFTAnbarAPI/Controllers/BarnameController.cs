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
    public class BarnameController : ControllerBase
    {
        private readonly IBarnameService _service;
        public BarnameController(IBarnameService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<GridData<BarnameDTO>>> GetBarname([FromQuery] string queryParams)
        {
            var qParams = JsonConvert.DeserializeObject<GridData<BarnameDTO>>(queryParams);
            return Ok(await _service.Get(qParams));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BarnameDTO>> GetBarname(long id)
        {
            if (!_service.BarnameExists(id))
                return NotFound();

            return Ok(await _service.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBarname(long id, BarnameDTO barname)
        {
            if (id != barname.Id)
            {
                return BadRequest();
            }

            await _service.Update(barname);

            try
            {
                await _service.Save();
            }
            catch (DbUpdateConcurrencyException) when (!_service.BarnameExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> CreateBarname(BarnameDTO dto)
        {
            _service.Create(dto);
            await _service.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBarname(long id)
        {
            await _service.Delete(id);
            await _service.Save();
            return NoContent();
        }
    }
}