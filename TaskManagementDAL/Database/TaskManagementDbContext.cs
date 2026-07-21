using Microsoft.EntityFrameworkCore;

using TaskManagementDAL.Entities;

namespace TaskManagementDAL.Database
{
    public class TaskManagementDbContext:DbContext
    {
        public TaskManagementDbContext()
        {

        }
        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options) : base(options) { }
        public DbSet<task> Task { get; set; }
        public DbSet<project> Project { get; set; }
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
