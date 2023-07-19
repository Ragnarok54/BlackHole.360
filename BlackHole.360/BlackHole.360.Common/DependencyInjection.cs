using BlackHole._360.Common.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlackHole._360.Common;
public static class DependencyInjection
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<VersionOptions>(configuration.GetSection(VersionOptions.OptionLocation));

        return services;
    }
}
