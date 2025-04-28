
using Microsoft.EntityFrameworkCore;
using YouDo.API.Middlewares;
using YouDo.Infraestructure.Data.Context;
using YouDo.Infraestructure.IoC;

namespace YouDo.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;

            services.AddInfrastructure(builder.Configuration);
            services.AddInfrastructureJWT(builder.Configuration);
            services.AddInfrastructureSwagger();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddPolicy("YouDoApp",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200");
                        policy.AllowAnyMethod();
                        policy.AllowAnyHeader();
                    });
            });

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            InitializeDatabase(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseCors("YouDoApp");
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void InitializeDatabase(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();

                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Ocorreu um erro ao aplicar as migrações.");
                }
            }
        }
    }
}
