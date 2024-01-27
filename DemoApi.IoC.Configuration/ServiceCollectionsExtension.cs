using DemoApi.Application;
using DemoApi.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DemoApi.IoC.Configuration;

public static class ServiceCollectionsExtension
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepository(configuration);
        services.AddApplicationServices(configuration);
        return services;
    }
}
