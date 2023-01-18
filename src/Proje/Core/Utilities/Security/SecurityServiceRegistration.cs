using Core.Utilities.Security.Jwt;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.Security
{
    public static class SecurityServiceRegistration
    {
        public static IServiceCollection AddSecurityServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHelper, JwtHelper>();
            return services;
        }
    }
}
