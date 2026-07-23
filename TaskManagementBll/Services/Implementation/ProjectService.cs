using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementBLL.DTOs.ProjectDTOs;
using TaskManagementBLL.DTOs.ServiceResponseDTOs;
using TaskManagementBLL.DTOs.TaskDTOs;
using TaskManagementBLL.Mapperly.Project;
using TaskManagementBLL.Mapperly.Task;
using TaskManagementBLL.Services.Abstraction;
using TaskManagementDAL.Entities;
using TaskManagementDAL.FilterModels.Project;
using TaskManagementDAL.Repository.Abstraction;

namespace TaskManagementBLL.Services.Implementation
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ServiceResponse<ProjectResponseDto>> AddProjectAsync(CreateProjectDto Project)
        {
            
            project mappedEntity = new CreateProject().MapToProject(Project);

            project result = await _projectRepository.AddProjectAsync(mappedEntity);

            if (result == null)
            {
                return new ServiceResponse<ProjectResponseDto>
                {
                    Data = null,
                    Message = "Failed to add the project.",
                    Success = false
                };
            }

           
            return new ServiceResponse<ProjectResponseDto>
            {
                Data = new ProjectResponse().MapToProjectResponseDto(result),
                Message = "The project was added successfully.",
                Success = true
            };
        }

        public async Task<ServiceResponse< TaskResponseDto>> AddProjectTaskAsync(CreateTaskDto task, int projectId)
        {
            task mappedEntity = new CreateTask().MapToTask(task);
            var result=await _projectRepository.AddProjectTaskAsync(mappedEntity, projectId);
            if (result == null)
            {
                return new ServiceResponse<TaskResponseDto>
                {
                    Data = null,
                    Message = "Failed to add the task.",
                    Success = false
                };
            }


            return new ServiceResponse<TaskResponseDto>
            {
                Data = new TaskResponse().MapToTaskResponseDto(result),
                Message = "The task was added successfully.",
                Success = true
            };
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            bool result = await _projectRepository.DeleteAsync(id);
            
            if (!result)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Message = "Failed to delete the project.",
                    Success = false
                };
            }


            return new ServiceResponse<bool>
            {
                Data =true,
                Message = "The project was added successfully.",
                Success = true
            };
        }

        public async Task<ServiceResponse<IEnumerable<ProjectResponseDto>>> GetAllAsync(ProjectQueryParameters query)
        {
            
            var result=await _projectRepository.GetAllAsync(query);
            IEnumerable<ProjectResponseDto> item1= new ProjectResponse().MapToProjectDtoList(result.Item1.ToList());
            if(result.Item2==0)
            {
                return new ServiceResponse<IEnumerable<ProjectResponseDto>>
                {
                    Data = null,
                    Count = result.Item2,
                    Message = "Failed to find the projects.",
                    Success = false
                };
            }
            return new ServiceResponse<IEnumerable<ProjectResponseDto>>
            {
                Data =item1, 
                Count=result.Item2,
                Message = "The projects are founded successfully.",
                Success = true
            };
        }

        public async Task<ServiceResponse<ProjectResponseDto>> GetByIdAsync(int id)
        {
            project result = await _projectRepository.GetByIdAsync(id);
           
            if (result == null)
            {
                return new ServiceResponse<ProjectResponseDto>
                {
                    Data = null,
                    Message = "Failed to find the project.",
                    Success = false
                };
            }


            return new ServiceResponse<ProjectResponseDto>
            {
                Data = new ProjectResponse().MapToProjectResponseDto(result),
                Message = "The project is founded successfully.",
                Success = true
            };
        }

        public async Task<ServiceResponse<ProjectResponseDto>> UpdateAsync(UpdateProjectDto Project,int projectId)
        {
            project mappedEntity = new UpdateProject().MapToProject(Project);
            mappedEntity.Id = projectId;

            project result = await _projectRepository.UpdateAsync(mappedEntity);

            if (result == null)
            {
                return new ServiceResponse<ProjectResponseDto>
                {
                    Data = null,
                    Message = "Failed to update the project.",
                    Success = false
                };
            }

          
            return new ServiceResponse<ProjectResponseDto>
            {
                Data = new ProjectResponse().MapToProjectResponseDto(result),
                Message = "The project was updated successfully.",
                Success = true
            };
        }
    }
}
