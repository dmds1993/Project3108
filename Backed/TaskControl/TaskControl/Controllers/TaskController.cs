using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Domain.Interfaces.Service;
using Microsoft.AspNetCore.Cors;
using Domain.Services;

namespace TaskControl.Controllers
{
    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPBIById(int id)
        {
            var taskModel = await _taskService.GetByIdAsync(id);

            if (taskModel != null)
            {
                return Ok(taskModel);
            }
            else
            {
                return NotFound($"PBI with ID {id} not found");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTasks(List<TaskModel> taskModels)
        {
            var response = await _taskService.CreateAsync(taskModels);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskModel taskModel)
        {
            var response = await _taskService.UpdateAsync(id, taskModel);

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
        public async Task<IActionResult> DeleteTask(int id)
        {
            var response = await _taskService.DeleteAsync(id);

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
