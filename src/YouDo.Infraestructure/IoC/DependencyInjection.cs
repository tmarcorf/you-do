using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YouDo.Application.Interfaces;
using YouDo.Application.Services;
using YouDo.Core.Account;
using YouDo.Core.Interfaces;
using YouDo.Infraestructure.Data.Context;
using YouDo.Infraestructure.Data.Identity;
using YouDo.Infraestructure.Data.Repositories;

namespace YouDo.Infraestructure.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable(configuration["DefaultConnection"]);

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseNpgsql(connectionString, 
                options => options.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
            );

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
            });

            services.AddScoped<IToDoRepository, ToDoRepository>();

            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IToDoService, ToDoService>();

            return services;
        }
    }
}
