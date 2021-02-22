using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NFTAnbarAPI.Models;
using NFTAnbarAPI.DTOs;
using NFTAnbarAPI.Services;
using Newtonsoft.Json;

namespace NFTAnbarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NdepoController : ControllerBase
    {
        private readonly INdepoService _service;
        public NdepoController(INdepoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<GridData<NdepoDTO>>> GetNdepo([FromQuery] string queryParams)
        {
            var qParams = JsonConvert.DeserializeObject<GridData<NdepoDTO>>(queryParams);
            return Ok( await _service.Get(qParams));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NdepoDTO>> GetNdepo(long id)
        {
            var depo = await _service.GetById(id);
            if (depo == null)
                return NotFound();

            return Ok(depo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNdepo(long id, NdepoDTO depo)
        {
            if (depo.Id != id)
                return BadRequest();

            await _service.Update(depo);

            try
            {
                await _service.Save();
            }
            catch (DbUpdateConcurrencyException) when (!_service.NdepoExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> CreateNdepo(NdepoDTO depo)
        {
            _service.Create(depo);
            await _service.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNdepo(long id)
        {
            await _service.Delete(id);
            await _service.Save();
            return NoContent();
        }
    }
}