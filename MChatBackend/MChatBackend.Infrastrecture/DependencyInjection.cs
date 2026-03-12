using MChatBackend.Core.ReposetryContracts;
using MChatBackend.Infrastrecture.DBContext;
using MChatBackend.Infrastrecture.Reposetries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace MChatBackend.Infrastrecture;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastrecture(this IServiceCollection services,IConfiguration configuration) 
        {
        services.AddScoped<IAuthReposetry, AuthReposetry>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddDbContext<ApplicationDbContext>(options =>options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        return services;
        }

    }

