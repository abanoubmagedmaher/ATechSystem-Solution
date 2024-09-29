
using ATechSystem.Models;
using ATechSystem.Repository;
using Microsoft.EntityFrameworkCore;

namespace ATechSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            #region Register DbContext
            builder.Services.AddDbContext<ATechSystemContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ATechSystem_db"));
            });
            #endregion

            #region Register Repository
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();  
            #endregion

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
