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
    public partial class UpdateProject
    {
        public partial UpdateProjectDto MapToUpdateProjectDto(project project);
        public partial project MapToProject(UpdateProjectDto project);
        public partial List<UpdateProjectDto> MapToProjectDtoList(List<project> projects);

    }
}
