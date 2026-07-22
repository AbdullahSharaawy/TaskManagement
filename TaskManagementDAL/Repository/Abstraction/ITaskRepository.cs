using System;
using System.Collections.Generic;
using System.Linq;

using TaskManagementDAL.Entities;
using TaskManagementDAL.FilterModels.Task;

namespace TaskManagementDAL.Repository.Abstraction
{
    public interface ITaskRepository
    {
      
      
        Task<task?> GetByIdAsync(int id);
       
        Task<task?> UpdateAsync(task task);
        Task<bool> DeleteAsync(int id);
        Task<(List<task> Items, int TotalCount)> GetAllAsync(TaskQueryParameters query, int? projectId = null);

    }
}
