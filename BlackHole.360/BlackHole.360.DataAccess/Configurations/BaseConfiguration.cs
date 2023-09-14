using BlackHole._360.Domain.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlackHole._360.DataAccess.Configurations;

internal static class BaseConfiguration
{
    public static EntityTypeBuilder<T> ConfigureSoftDelete<T>(this EntityTypeBuilder<T> builder) where T : class, ISoftDelete
    {
        builder.HasQueryFilter(e => !e.Deleted);

        return builder;
    }
}
