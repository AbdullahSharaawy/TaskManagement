
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TaskManagementDAL.Enums;

namespace TaskManagementDAL.Entities
{
    public class task
    {
        [Key]
        public int Id { get; set; }
        public string title { get; set; }=string.Empty;
        public string? description { get; set; }
        public Status status { get; set; }=Status.todo;
        public Priority priority { get; set; }=Priority.medium;
        public DateTime? due_date { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        [ForeignKey("project")]
        public int project_id { get; set; }
        public project project { get; set; }
    }
}
