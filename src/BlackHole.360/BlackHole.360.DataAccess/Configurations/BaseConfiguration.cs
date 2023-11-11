using BlackHole._360.Domain.Abstractions.Entities;
using BlackHole._360.Domain.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlackHole._360.DataAccess.Configurations;

internal static class BaseConfiguration
{
    public static EntityTypeBuilder<T> ConfigureEntityTable<T>(this EntityTypeBuilder<T> builder) where T : class
    {
        builder.ToTable(typeof(T).Name);

        return builder;
    }

    public static EntityTypeBuilder<T> ConfigureBaseEntity<T>(this EntityTypeBuilder<T> builder) where T : BaseEntity
    {
        builder.ConfigureEntityTable()
               .HasKey(entity => entity.Id);

        return builder;
    }

    public static EntityTypeBuilder<T> ConfigureSoftDelete<T>(this EntityTypeBuilder<T> builder) where T : class, ISoftDelete
    {
        builder.HasQueryFilter(e => !e.Deleted);

        return builder;
    }
}
