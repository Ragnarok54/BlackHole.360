using BlackHole._360.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlackHole._360.DataAccess.Configurations;

internal class JobTitleConfiguration : IEntityTypeConfiguration<JobTitle>
{
    public void Configure(EntityTypeBuilder<JobTitle> builder)
    {
        builder.ConfigureEntityTable();
        
        builder.HasKey(jt => (int)jt.Id);

        builder.HasData(Enum.GetValues(typeof(Domain.Enums.JobTitle))
                            .Cast<Domain.Enums.JobTitle>()
                            .Select(value => new JobTitle
                            {
                                Id = value,
                                Name = Enum.GetName(typeof(Domain.Enums.JobTitle), value) ?? string.Empty
                            }));
    }
}
