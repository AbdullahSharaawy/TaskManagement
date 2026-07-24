
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Text.Json.Serialization;
using TaskManagementBLL.Services.Abstraction;
using TaskManagementBLL.Services.Implementation;
using TaskManagementDAL.Database;
using TaskManagementDAL.Repository.Abstraction;
using TaskManagementDAL.Repository.Implementation;

namespace TaskManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                // This converts all enums to strings globally
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            }); 
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // register Task and Project repositories in DI container
            builder.Services.AddScoped<ITaskRepository,TaskRepository>();
            builder.Services.AddScoped<IProjectRepository,ProjectRepository>();
            // register Task and Project services in DI container
            builder.Services.AddScoped<IProjectService,ProjectService>();
            builder.Services.AddScoped<ITaskService,TaskService>();
           // database connection
            var con = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<TaskManagementDbContext>(options =>
            options.UseSqlServer(
                    con,
                    b => b.MigrationsAssembly("TaskManagementDAL")  // Specify migrations assembly here
                )
            );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
