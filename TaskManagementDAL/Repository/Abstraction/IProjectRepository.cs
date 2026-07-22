using System;
using System.Collections.Generic;

using TaskManagementDAL.Entities;
using TaskManagementDAL.FilterModels.Project;

namespace TaskManagementDAL.Repository.Abstraction
{
    public interface IProjectRepository
    {
        Task<project?> GetByIdAsync(int id);
        Task<(IEnumerable<project>, int)> GetAllAsync(ProjectQueryParameters query);
        Task<task?> AddProjectTaskAsync(task task,int projectId);
        Task<project?> AddProjectAsync(project project);
        Task<project?> UpdateAsync(project project);
        Task<bool> DeleteAsync(int id);

    }
}
