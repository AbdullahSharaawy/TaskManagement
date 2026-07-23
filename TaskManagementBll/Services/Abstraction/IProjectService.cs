using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementBLL.DTOs.ProjectDTOs;
using TaskManagementBLL.DTOs.ServiceResponseDTOs;
using TaskManagementBLL.DTOs.TaskDTOs;
using TaskManagementDAL.Entities;
using TaskManagementDAL.FilterModels.Project;

namespace TaskManagementBLL.Services.Abstraction
{
    public interface IProjectService
    {
        Task<ServiceResponse<ProjectResponseDto>> GetByIdAsync(int id);
        public  Task<ServiceResponse<IEnumerable<ProjectResponseDto>>> GetAllAsync(ProjectQueryParameters query);
        Task<ServiceResponse<ProjectResponseDto>> AddProjectAsync(CreateProjectDto Project);
        Task<ServiceResponse<ProjectResponseDto>> UpdateAsync(UpdateProjectDto Project,int projectId);
        Task<ServiceResponse< bool>> DeleteAsync(int id);
        Task<ServiceResponse<TaskResponseDto>> AddProjectTaskAsync(CreateTaskDto task, int projectId);
    }
}
