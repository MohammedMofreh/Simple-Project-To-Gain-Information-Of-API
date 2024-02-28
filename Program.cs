
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebAPI.Models;

namespace WebAPI
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
            // Add The Services "Swagger" >> Documentation Of Our APIs 
            builder.Services.AddSwaggerGen(options =>
            {
                //Add | Modify Some Info about Swagger
                options.SwaggerDoc("v1", new OpenApiInfo

                {
                    Version = "v1",
                    Title = "Test Our APIs",
                    Description = "Simple Project To Gain Information Of API",
                    TermsOfService = new Uri("https://www.linkedin.com/in/mohammed-saad-el-din-7a98181b6"),

                    Contact = new OpenApiContact
                    {
                        // Developer Name
                        Name = "Mohammed Mofreh",
                        // Contact With the Developer
                        Email = "mohamedmofreh236@gmail.com"
                    }
                });

                //Add Security Part to Swagger
                //JWT >> Bearer
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter K",

                }
                    );
                // add autho to each end_point
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string> ()
                    }
                });

            }

            );
            builder.Services.AddDbContext<NorthwindContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}