using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementDAL.Enums.Filter;
using TaskManagementDAL.Enums;

namespace TaskManagementDAL.FilterModels.Task
{
    public class TaskQueryParameters
    {
        public Status? Status { get; set; }
        public Priority? Priority { get; set; }
        public DateTime? DueDateFrom { get; set; }
        public DateTime? DueDateTo { get; set; }
        public string? Search { get; set; }
        public SortBy SortBy { get; set; } = SortBy.created_at;
        public SortDirection SortDirection { get; set; } = SortDirection.desc;
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
    }
}
