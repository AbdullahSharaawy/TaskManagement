using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementBLL.DTOs.ServiceResponseDTOs
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)Limit);
    }

}
