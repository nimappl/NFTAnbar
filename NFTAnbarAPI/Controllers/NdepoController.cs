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
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NdepoDTO>> GetNdepo(long id)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNdepo(long id)
        {
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> CreateNdepo(NdepoDTO dto)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNdepo(long id)
        {
            return NoContent();
        }
    }
}