using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces.Service;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TaskControl.Controllers
{
    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    public class PBIController : ControllerBase
    {
        private readonly IPBIService _pbiService;

        public PBIController(IPBIService pbiService)
        {
            _pbiService = pbiService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPBIById(int id)
        {
            var pbi = await _pbiService.GetByIdAsync(id);

            if (pbi != null)
            {
                return Ok(pbi);
            }
            else
            {
                return NotFound($"PBI with ID {id} not found");
            }
        }

        [HttpGet("")]
        public async Task<IActionResult> GetPBIsWithTasks()
        {
            var pbiWithTasksList = await _pbiService.GetPBIsWithTasksAsync();
            return Ok(pbiWithTasksList);
        }

        [HttpPost()]
        public async Task<IActionResult> CreatePbi([FromBody] PBIModel pbiModel)
        {
            var pbi = await _pbiService.CreatePbi(pbiModel);

            if (pbi != null)
            {
                return Ok(pbi);
            }
            else
            {
                return NotFound($"PBI with ID {pbiModel.Name} not found");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePBI(int id, [FromBody] PBIModel pbiModel)
        {
            var response = await _pbiService.UpdateAsync(id, pbiModel);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePBI(int id)
        {
            var response = await _pbiService.RemoveAsync(id);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
