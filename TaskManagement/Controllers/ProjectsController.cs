using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementBLL.DTOs.ProjectDTOs;
using TaskManagementBLL.DTOs.TaskDTOs;
using TaskManagementBLL.Services.Abstraction;
using TaskManagementBLL.Services.Implementation;
using TaskManagementDAL.FilterModels.Project;
using TaskManagementDAL.FilterModels.Task;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IProjectService _projectService;
        public ProjectsController(ITaskService taskService, IProjectService projectService)
        {
            _taskService = taskService;
            _projectService = projectService;
        }

        [HttpGet("{id}/tasks")]
        public async Task<IActionResult> GetProjectTasks(int id, [FromQuery] TaskQueryParameters query)
        {
            var result = await _taskService.GetAllAsync(query, projectId: id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProjects([FromQuery] ProjectQueryParameters query)
        {
            var result = await _projectService.GetAllAsync(query);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectDto Project)
        {
            var result=await _projectService.AddProjectAsync(Project);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(UpdateProjectDto Project,int id)
        {
            var result=await _projectService.UpdateAsync(Project,id);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var result=await _projectService.GetByIdAsync(id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var result=await _projectService.DeleteAsync(id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }
        [HttpPost("{id}/tasks")]
        public async Task<IActionResult> CreateProjectTask(CreateTaskDto task,int id)
        {
            var result=await _projectService.AddProjectTaskAsync(task, id);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
