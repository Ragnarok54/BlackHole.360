using BlackHole._360.DataAccess.Repositories;
using BlackHole._360.Domain.Entities;
using BlackHole._360.Domain.Interfaces;
using BlackHole._360.Domain.Interfaces.Repositories;
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
        services.AddScoped<IRepository<User>, Repository<User>>();

        return services;
    }
}
