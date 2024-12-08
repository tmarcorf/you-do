using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), 
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
            );

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(
                options => options.AccessDeniedPath = "/Account/Login");

            services.AddScoped<IToDoRepository, ToDoRepository>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();

            return services;
        }
    }
}
