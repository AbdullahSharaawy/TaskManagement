using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementDAL.Enums;

namespace TaskManagementBLL.DTOs.TaskDTOs
{
    public class UpdateTaskDto
    {
        public string title { get; set; } = string.Empty;
        public string? description { get; set; }
        public Status status { get; set; } = Status.todo;
        public Priority priority { get; set; } = Priority.medium;
        public DateTime? due_date { get; set; }

    }
}
