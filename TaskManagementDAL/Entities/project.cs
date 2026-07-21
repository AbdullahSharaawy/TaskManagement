using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace TaskManagementDAL.Entities
{
    public class project
    {
        [Key]
        public int Id { get; set; }
       
        public string name { get; set; }=string.Empty;
        public string? description {  get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public ICollection<task> tasks { get; set; }
    }
}
