using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementBLL.DTOs.TaskDTOs;

namespace TaskManagementBLL.DTOs.ProjectDTOs
{
    public class ProjectResponseDto
    {
        public int Id { get; set; }

        public string name { get; set; } = string.Empty;
        public string? description { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public ICollection<TaskResponseDto> tasks { get; set; }
    }
}
