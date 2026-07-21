using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementDAL.Entities;

namespace TaskManagementDAL.Repository.Abstraction
{
    public interface IProjectRepository
    {
        Task<project> GetByIdAsync(int id);
        Task<IEnumerable<project>> GetAllAsync();
        Task<project> AddAsync(project project);
        Task<project> UpdateAsync(project project);
        Task<bool> DeleteAsync(int id);

    }
}
