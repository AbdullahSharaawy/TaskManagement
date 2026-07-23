using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementBLL.DTOs.ProjectDTOs;
using TaskManagementBLL.DTOs.TaskDTOs;
using TaskManagementBLL.Services.Abstraction;
using TaskManagementBLL.Services.Implementation;
using TaskManagementDAL.FilterModels.Task;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TaskQueryParameters query)
        {
            var result = await _taskService.GetAllAsync(query);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }
       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(UpdateTaskDto Task,int id)
        {
            var result = await _taskService.UpdateAsync(Task, id);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var result = await _taskService.GetByIdAsync(id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result = await _taskService.DeleteAsync(id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

    }
}
