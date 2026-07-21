using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementDAL.Entities;

namespace TaskManagementDAL.Database
{
    public class TaskManagementDbContext:DbContext
    {
        public TaskManagementDbContext()
        {

        }
        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options) : base(options) { }
        DbSet<task> Task { get; set; }
        DbSet<project> Project { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<project>(entity =>
            {
                entity.Property(p=>p.name).IsRequired();
                entity.HasIndex(p => p.name).IsUnique();
                entity.HasMany(p => p.tasks).WithOne(t => t.project).HasForeignKey(t => t.project_id).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<task>(entity =>
            {
                entity.Property(p=>p.project_id).IsRequired();
                entity.Property(p=>p.title).IsRequired();
            });
        }
    }
}
