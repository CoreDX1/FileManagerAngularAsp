using File.Infrastructure.Persistences.Context;
using File.Infrastructure.Persistences.Interfaces;
using File.Infrastructure.Persistences.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace File.Infrastructure.Extensions;

public static class InjectionExtensions
{
    public static IServiceCollection AddInjectionInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var assembly = typeof(FileManagerContext).Assembly.FullName;
        services.AddDbContext<FileManagerContext>(
            options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("FileConnectionString"),
                    b => b.MigrationsAssembly(assembly)
                ),
            ServiceLifetime.Transient
        );
        services.AddTransient<IFolderRepository, FolderRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
