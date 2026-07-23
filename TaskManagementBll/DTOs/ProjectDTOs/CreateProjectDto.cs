using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementBLL.DTOs.ProjectDTOs
{
    public class CreateProjectDto
    {
        public string name { get; set; } = string.Empty;
        public string? description { get; set; }
    }
}
