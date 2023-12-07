using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbInfra(configuration);
            services.AddJwtExtension(configuration);
            services.AddSwaggerExtension();
            services.AddRequestHandlers();
            services.AddMapping();

            return services;
        }


    }
}
