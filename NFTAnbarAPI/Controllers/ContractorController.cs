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
    public class ContractorController : ControllerBase
    {
        private readonly IContractorService _service;
        public ContractorController(IContractorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<GridData<ContractorDTO>>> GetContractor([FromQuery] string queryParams)
        {
            var qParams = JsonConvert.DeserializeObject<GridData<ContractorDTO>>(queryParams);
            return Ok(await _service.Get(qParams));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContractorDTO>> GetContractor(long id)
        {
            if (!_service.ContractorExists(id))
                return NotFound();

            return Ok(await _service.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateContractor(long id, ContractorDTO contractor)
        {
            if (id != contractor.Id)
            {
                return BadRequest();
            }

            await _service.Update(contractor);

            try
            {
                await _service.Save();
            }
            catch (DbUpdateConcurrencyException) when (!_service.ContractorExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> CreateContractor(ContractorDTO dto)
        {
            _service.Create(dto);
            await _service.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteContractor(long id)
        {
            await _service.Delete(id);
            await _service.Save();
            return NoContent();
        }
    }
}