using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementDAL.Repository.Abstraction
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Task>> GetAllTasks();
        Task<IEnumerable<Task>> GetTasksByProjectId(int projectId);
        Task<Task> GetTaskById(int id);
        Task<Task> AddAsync(Task task);
        Task<Task> UpdateAsync(Task task);
        Task<bool> DeleteAsync(int id);

    }
}
