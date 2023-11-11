using BlackHole._360.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlackHole._360.DataAccess.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ConfigureBaseEntity();
        builder.ConfigureSoftDelete();

        builder.HasOne(u => u.SubGroup)
               .WithMany(sg => sg.Users)
               .HasPrincipalKey(sg => sg.Id)
               .HasForeignKey(u => u.SubgroupId);
    }
}
