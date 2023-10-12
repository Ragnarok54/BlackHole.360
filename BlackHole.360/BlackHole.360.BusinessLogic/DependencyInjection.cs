﻿using BlackHole._360.BusinessLogic.Services;

using Microsoft.Extensions.DependencyInjection;

namespace BlackHole._360.BusinessLogic;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<UserService>();
        services.AddScoped<GroupService>();
        services.AddScoped<ImportService>();

        return services;
    }
}
