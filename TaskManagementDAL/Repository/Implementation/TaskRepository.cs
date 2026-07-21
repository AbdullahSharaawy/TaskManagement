using Microsoft.EntityFrameworkCore;
using TaskManagementDAL.Database;
using TaskManagementDAL.Entities;
using TaskManagementDAL.Repository.Abstraction;

namespace TaskManagementDAL.Repository.Implementation
{
    public class TaskRepository:ITaskRepository
    {
        private readonly TaskManagementDbContext _dbContext;
        public TaskRepository(TaskManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<task?> AddAsync(task task)
        {
            _dbContext.Add(task);
            int RowAffected = await _dbContext.SaveChangesAsync();
            if (RowAffected > 0)
                return task;
            return null;
        }

      
        public async Task<bool> DeleteAsync(int id)
        {
            task? project = await GetByIdAsync(id);
            if (project != null)
            {
                _dbContext.Remove(project);
                int RowAffected = await _dbContext.SaveChangesAsync();
                if (RowAffected > 0)
                    return true;
                return false;

            }
            else
                return false;
        }

        public async Task<IEnumerable<task>> GetAllAsync()
        {
            return await _dbContext.Task.ToListAsync();
        }

        public async Task<task?> GetByIdAsync(int id)
        {
            return await _dbContext.Task.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<task>> GetTasksByProjectIdAsync(int projectId)
        {
            return await _dbContext.Task.Where(t => t.project_id == projectId).ToListAsync();
        }

        public async Task<task?> UpdateAsync(task task)
        {
            _dbContext.Update(task);
            int RowAffected = await _dbContext.SaveChangesAsync();
            if (RowAffected > 0)
                return task;
            return null;
        }

      
    }
}
 