using DemoApi.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static DemoApi.Infrastructure.Constants;

namespace DemoApi.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddRepository(this  IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>((s, builder) =>
        {
            //builder.UseSqlServer(configuration[ApplicationConstants.DefaultConnection]);
            builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            builder.UseLoggerFactory(s.GetRequiredService<ILoggerFactory>());
            builder.LogTo(Console.WriteLine,LogLevel.Debug);
        },ServiceLifetime.Scoped);
        return services;
    }
}
