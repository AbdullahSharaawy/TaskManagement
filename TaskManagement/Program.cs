
using Microsoft.EntityFrameworkCore;
using TaskManagementDAL.Database;

namespace TaskManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
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
