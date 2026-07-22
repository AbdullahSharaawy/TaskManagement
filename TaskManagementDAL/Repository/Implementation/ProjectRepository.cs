using Microsoft.EntityFrameworkCore;

using TaskManagementDAL.Database;
using TaskManagementDAL.Entities;
using TaskManagementDAL.FilterModels.Project;
using TaskManagementDAL.Repository.Abstraction;

namespace TaskManagementDAL.Repository.Implementation
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly TaskManagementDbContext _dbContext;
        public ProjectRepository(TaskManagementDbContext dbContext) 
        {
          _dbContext = dbContext;
        }
        public async Task<project?>  AddProjectAsync(project project)
        {
            _dbContext.Add(project);
            
            int RowAffected= await _dbContext.SaveChangesAsync();
            if (RowAffected > 0)
                return project;
            return null;
        }

        public async Task<task?> AddProjectTaskAsync(task task, int projectId)
        {
            var project = await _dbContext.Project.Where(p => p.Id == projectId).FirstOrDefaultAsync();
            if (project == null) return null;

            task.project_id = projectId;
            _dbContext.Add(task);
            int RowAffected = await _dbContext.SaveChangesAsync();
            if (RowAffected > 0)
                return task;
            return null;
        }

        public async Task<bool> DeleteAsync(int id)
        {

            project? project=await GetByIdAsync(id);
            if (project != null)
            {
                _dbContext.Remove(project);
               int RowAffected=await _dbContext.SaveChangesAsync();
                if (RowAffected > 0)
                    return true;
                return false;
            
            }else
                return false;
                
            
        }

        public async Task<(IEnumerable<project>, int)> GetAllAsync(ProjectQueryParameters query)

        {
            IQueryable<project> projects = _dbContext.Project
          .AsNoTracking()
          .OrderByDescending(p => p.created_at); 

            int totalCount = await projects.CountAsync();

            var items = await projects
                .Skip((query.Page - 1) * query.Limit)
                .Take(query.Limit)
                .ToListAsync();

            return (items, totalCount);

        }

        public async Task<project?> GetByIdAsync(int id)
        {

            return await _dbContext.Project.Where(p=>p.Id==id).FirstOrDefaultAsync();
        }

        public async Task<project?> UpdateAsync(project project)
        {
            _dbContext.Update(project);
            int RowAffected= await _dbContext.SaveChangesAsync();
            if (RowAffected > 0)
                return project;
            return null;
        }
    }
}
