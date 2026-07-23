using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementBLL.DTOs.TaskDTOs;
using TaskManagementDAL.Entities;

namespace TaskManagementBLL.Mapperly.Task
{
    [Mapper]
    public partial class UpdateTask
    {
        public partial UpdateTaskDto MapToUpdateTaskDto(task task);
        public partial task MapToTask(UpdateTaskDto task);
        public partial List<UpdateTaskDto> MapToTaskDtoList(List<task> tasks);


    }
}
