using Microsoft.EntityFrameworkCore;
using TaskManagementDAL.Database;
using TaskManagementDAL.Entities;
using TaskManagementDAL.FilterModels.Task;
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

        public async Task<(List<task> Items, int TotalCount)> GetAllAsync(TaskQueryParameters query, int? projectId = null)
        {
            IQueryable<task> tasks = _dbContext.Task
       .Include(t => t.project)
       .AsNoTracking();

            if (projectId.HasValue)
                tasks = tasks.Where(t => t.project_id == projectId.Value);

            if (query.Status.HasValue)
            {
                tasks = tasks.Where(t => t.status == query.Status.Value);
            }

            if (query.Priority.HasValue)
                tasks = tasks.Where(t => t.priority == query.Priority);

            if (query.DueDateFrom.HasValue)
                tasks = tasks.Where(t => t.due_date >= query.DueDateFrom.Value);

            if (query.DueDateTo.HasValue)
                tasks = tasks.Where(t => t.due_date <= query.DueDateTo.Value);

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                var term = query.Search.Trim();
                tasks = tasks.Where(t =>
                    EF.Functions.Like(t.title, $"%{term}%") ||
                    (t.description != null && EF.Functions.Like(t.description, $"%{term}%")));
            }

            // Sorting
            bool desc = query.SortDirection == Enums.Filter.SortDirection.desc;
            tasks = query.SortBy switch
            {
                Enums.Filter.SortBy.due_date => desc ? tasks.OrderByDescending(t => t.due_date) : tasks.OrderBy(t => t.due_date),
                Enums.Filter.SortBy.priority => desc ? tasks.OrderByDescending(t => t.priority) : tasks.OrderBy(t => t.priority),
                _ => desc ? tasks.OrderByDescending(t => t.created_at) : tasks.OrderBy(t => t.created_at),
            };

            int totalCount = await tasks.CountAsync();

            var items = await tasks
                .Skip((query.Page - 1) * query.Limit)
                .Take(query.Limit)
                .ToListAsync();

            return (items, totalCount);
        }
        public async Task<task?> GetByIdAsync(int id)
        {
            return await _dbContext.Task.Where(p => p.Id == id).FirstOrDefaultAsync();
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
 