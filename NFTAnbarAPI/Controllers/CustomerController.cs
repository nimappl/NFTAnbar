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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;
        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<GridData<CustomerDTO>>> GetCustomer([FromQuery] string queryParams)
        {
            var qParams = JsonConvert.DeserializeObject<GridData<CustomerDTO>>(queryParams);
            return Ok(await _service.Get(qParams));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(long id)
        {
            return Ok(await _service.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(long id, CustomerDTO customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            await _service.Update(customer);

            try
            {
                await _service.Save();
            }
            catch (DbUpdateConcurrencyException) when (!_service.CustomerExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomer(CustomerDTO customer)
        {
            _service.Create(customer);
            await _service.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(long id)
        {
            await _service.Delete(id);
            await _service.Save();
            return NoContent();
        }
    }
}