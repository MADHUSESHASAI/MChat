using MChatBackend.Core.ServiceContracts;
using MChatBackend.Core.Services;
using MChatBackend.Infrastrecture.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MChatBackend.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore (this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
