using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementBLL.DTOs.ProjectDTOs;
using TaskManagementDAL.Entities;

namespace TaskManagementBLL.Mapperly.Project
{
    [Mapper]
    public partial class CreateProject
    {
        public partial CreateProjectDto MapToCreateProjectDto(project project);
        public partial project MapToProject(CreateProjectDto project);
        public partial List<CreateProjectDto> MapToProjectsDtoList(List<project> projects);

    }
}
