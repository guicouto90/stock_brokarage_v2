using Application.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ResourceMapperProfile));
            return services;
        }
    }
}
