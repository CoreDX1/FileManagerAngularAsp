using System.Reflection;
using File.Application.Interface;
using File.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace File.Application.Extensions;

public static class InjectionExtensions
{
    public static IServiceCollection AddInjectionApplication(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton(configuration);
        services.AddScoped<IFolderApplication, FolderApplication>();
        services.AddScoped<UtilsApplication>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}
