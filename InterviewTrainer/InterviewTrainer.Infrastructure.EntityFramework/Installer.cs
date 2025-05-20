using InterviewTrainer.Application.Abstractions.Repositories;
using InterviewTrainer.Infrastructure.EntityFramework.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InterviewTrainer.Infrastructure.EntityFramework;

public static class Installer
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DbSettings>(configuration.GetSection(nameof(DbSettings))); 
        services.AddDbContext<IUnitOfWork, DatabaseContext>();
        return services;
    }
}