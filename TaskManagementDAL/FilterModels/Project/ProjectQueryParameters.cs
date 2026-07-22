using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementDAL.FilterModels.Project
{
    public class ProjectQueryParameters
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 20;
    }
}
