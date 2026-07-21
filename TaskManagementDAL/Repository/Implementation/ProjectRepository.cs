using Microsoft.EntityFrameworkCore;

using TaskManagementDAL.Database;
using TaskManagementDAL.Entities;
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
        public async Task<project?>  AddAsync(project project)
        {
            _dbContext.Add(project);
            
            int RowAffected= await _dbContext.SaveChangesAsync();
            if (RowAffected > 0)
                return project;
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

        public async Task<IEnumerable<project>> GetAllAsync()
        {
            return await _dbContext.Project.ToListAsync();
            
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
