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
    public partial class ProjectResponse
    {
        public partial ProjectResponseDto MapToProjectResponseDto(project project);
        public partial project MapToProject(ProjectResponseDto project);
        public partial List<ProjectResponseDto> MapToProjectDtoList(List<project> projects);

    }
}
