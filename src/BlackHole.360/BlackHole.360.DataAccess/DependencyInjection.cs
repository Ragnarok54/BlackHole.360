using BlackHole._360.DataAccess.Abstractions;
using BlackHole._360.DataAccess.Abstractions.Repositories;
using BlackHole._360.DataAccess.Repositories;
using BlackHole._360.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlackHole._360.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database")
            ?? throw new InvalidOperationException("Attempted to connect to a database but no connection string was provided.");

        services.AddDbContext<BlackHoleContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPaginatedRepository<User>, PaginatedRepository<User>>();
        services.AddScoped<IRepository<Department>, Repository<Department>>();
        services.AddScoped<IRepository<Group>, Repository<Group>>();
        services.AddScoped<IRepository<SubGroup>, Repository<SubGroup>>();
        services.AddScoped<IFeedbackRepository, FeedbackRepository>();

        return services;
    }

    public static IHealthChecksBuilder AddDataAccessHealthChecks(this IHealthChecksBuilder healthCheckBuilder)
    {
        healthCheckBuilder.AddDbContextCheck<BlackHoleContext>();

        return healthCheckBuilder;
    }

    public static IServiceProvider MigrateDatabase(this IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<BlackHoleContext>();

            dbContext.Database.Migrate();
        }

        return serviceProvider;
    }
}
