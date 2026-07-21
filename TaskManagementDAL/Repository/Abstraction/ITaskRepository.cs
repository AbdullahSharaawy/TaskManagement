using System;
using System.Collections.Generic;
using System.Linq;

using TaskManagementDAL.Entities;

namespace TaskManagementDAL.Repository.Abstraction
{
    public interface ITaskRepository
    {
        Task<IEnumerable<task>> GetAllAsync();
        Task<IEnumerable<task>> GetTasksByProjectIdAsync(int projectId);
        Task<task?> GetByIdAsync(int id);
        Task<task?> AddAsync(task task);
        Task<task?> UpdateAsync(task task);
        Task<bool> DeleteAsync(int id);

    }
}
