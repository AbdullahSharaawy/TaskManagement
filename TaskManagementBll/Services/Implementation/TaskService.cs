using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementBLL.DTOs.TaskDTOs;
using TaskManagementBLL.DTOs.ServiceResponseDTOs;
using TaskManagementBLL.DTOs.TaskDTOs;
using TaskManagementBLL.Mapperly.Task;
using TaskManagementBLL.Services.Abstraction;
using TaskManagementDAL.Entities;
using TaskManagementDAL.Repository.Abstraction;
using TaskManagementDAL.Repository.Implementation;
using TaskManagementDAL.FilterModels.Task;
using Azure;

namespace TaskManagementBLL.Services.Implementation
{

    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

       
        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            bool result = await _taskRepository.DeleteAsync(id);

            if (!result)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Message = "Failed to delete the Task.",
                    Success = false
                };
            }


            return new ServiceResponse<bool>
            {
                Data = true,
                Message = "The Task was added successfully.",
                Success = true
            };
        }

      

        public async Task<ServiceResponse<TaskResponseDto>> GetByIdAsync(int id)
        {
            task result = await _taskRepository.GetByIdAsync(id);

            if (result == null)
            {
                return new ServiceResponse<TaskResponseDto>
                {
                    Data = null,
                    Message = "Failed to find the Task.",
                    Success = false
                };
            }


            return new ServiceResponse<TaskResponseDto>
            {
                Data = new TaskResponse().MapToTaskResponseDto(result),
                Message = "The Task is founded successfully.",
                Success = true
            };
        }

        public async Task<ServiceResponse<TaskResponseDto>> UpdateAsync(UpdateTaskDto Task,int taskId)
        {
            task mappedEntity = new UpdateTask().MapToTask(Task);
            mappedEntity.Id=taskId;
            task result = await _taskRepository.UpdateAsync(mappedEntity);

            if (result == null)
            {
                return new ServiceResponse<TaskResponseDto>
                {
                    Data = null,
                    Message = "Failed to update the Task.",
                    Success = false
                };
            }


            return new ServiceResponse<TaskResponseDto>
            {
                Data = new TaskResponse().MapToTaskResponseDto(result),
                Message = "The Task was updated successfully.",
                Success = true
            };
        }
        public async Task<ServiceResponse<PagedResult<TaskResponseDto>>> GetAllAsync(TaskQueryParameters query, int? projectId = null)
        {
           
            query.Page = query.Page < 1 ? 1 : query.Page;
            query.Limit = query.Limit  < 1  ? 20 : query.Limit;

        
            var (items, totalCount) = await _taskRepository.GetAllAsync(query, projectId);
            
            if(totalCount==0)
            {
                return new ServiceResponse<PagedResult<TaskResponseDto>>
                {
                    Data = null,
                    Message = "No tasks found.",
                    Success = false,
                    Count = 0
                };
            }


            var result=new PagedResult<TaskResponseDto>
           {
               Items = items.Select(t => new TaskResponseDto
            {
                Id = t.Id,
                project_id = t.project_id,
                project_name = t.project.name,
                title = t.title,
                description = t.description,
                status = t.status,
                priority = t.priority,
                due_date = t.due_date,
                created_at = t.created_at,
                updated_at = t.updated_at
            }).ToList(),
                TotalCount = totalCount,
                Page = query.Page,
                Limit = query.Limit
            };
            return new ServiceResponse<PagedResult<TaskResponseDto>>
            {
                Data = result,
                Message = "The tasks are founded successfully.",
                Success = true,
                Count=totalCount
            };
        }

      
    }
}
