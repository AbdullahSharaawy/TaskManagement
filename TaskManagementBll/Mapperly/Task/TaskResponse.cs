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
    public partial class TaskResponse
    {
        public partial TaskResponseDto MapToTaskResponseDto(task task);
        public partial task MapToTask(TaskResponseDto task);
        public partial List<TaskResponseDto> MapToTaskDtoList(List<task> tasks);


    }
}
