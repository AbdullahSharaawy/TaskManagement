using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementDAL.Enums;

namespace TaskManagementBLL.DTOs.TaskDTOs
{
    public class TaskResponseDto
    {
        public int Id { get; set; }
        public string title { get; set; } = string.Empty;
        public string? description { get; set; }
        public int project_id { get; set; }
        public Status status { get; set; } = Status.todo;
        public Priority priority { get; set; } = Priority.medium;
        public DateTime? due_date { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string project_name { get; set; } = string.Empty;
    }
}
