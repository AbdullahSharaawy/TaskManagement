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
    public partial class CreateTask
    {
        public partial CreateTaskDto MapToCreateTaskDto(task task);
        public partial task MapToTask(CreateTaskDto task);
        public partial List<CreateTaskDto> MapToTasksDtoList(List<task> tasks);


    }
}
