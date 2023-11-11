using BlackHole._360.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlackHole._360.DataAccess.Configurations;

internal class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.ConfigureBaseEntity();
        builder.ConfigureSoftDelete();

        builder.HasOne(f => f.FromUser)
               .WithMany(u => u.GivenFeedback)
               .HasPrincipalKey(u => u.Id)
               .HasForeignKey(f => f.FromUserId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(f => f.ToUser)
               .WithMany(u => u.ReceievdFeedback)
               .HasPrincipalKey(u => u.Id)
               .HasForeignKey(f => f.ToUserId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
