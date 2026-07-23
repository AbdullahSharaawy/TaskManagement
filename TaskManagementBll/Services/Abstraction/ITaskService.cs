using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementBLL.DTOs.TaskDTOs;
using TaskManagementBLL.DTOs.ServiceResponseDTOs;
using TaskManagementBLL.DTOs.TaskDTOs;
using TaskManagementDAL.Entities;
using TaskManagementDAL.FilterModels.Task;

namespace TaskManagementBLL.Services.Abstraction
{
    public interface ITaskService
    {
        Task<ServiceResponse<PagedResult<TaskResponseDto>>> GetAllAsync(TaskQueryParameters query, int? projectId = null);
        Task<ServiceResponse<TaskResponseDto>> GetByIdAsync(int id);
       
        Task<ServiceResponse<TaskResponseDto>> UpdateAsync(UpdateTaskDto task,int taskId);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
      

    }
}
