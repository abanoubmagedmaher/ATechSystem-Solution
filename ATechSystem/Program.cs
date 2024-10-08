
using ATechSystem.Models;
using ATechSystem.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ATechSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Handel Custom Validation Error To Show My Custom Validation !!!
            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                options.SuppressModelStateInvalidFilter=true); 
            #endregion

            #region Register DbContext
            builder.Services.AddDbContext<ATechSystemContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ATechSystem_db"));
            });
             builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ATechSystemContext>();

            #endregion

            #region Register Repository
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();
            #endregion

            #region Handel [Authontication] using JWT

            builder.Services.AddAuthentication(options => {

                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                // Handel [Authorize]
                options.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => // Verified Token 
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false; // If Send Throw Http if HTTPS = true
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer= "http://localhost:5219/",
                    ValidateAudience=true,
                    ValidAudience= "http://localhost:4200/",
                    IssuerSigningKey=
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("!@##%^465dg$%#$se563@$$Q$#%52$%#$5$%#$32143@#$@#$#%"))

                };

            });
            #endregion

            #region Handel CORSE 
            builder.Services.AddCors(optios => 
               {
                   optios.AddPolicy("ATeckPolicy", polict =>
                   {
                       polict.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
                       
                   });
                   
               }
            );
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

            app.UseStaticFiles();

            app.UseCors("ATeckPolicy");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
