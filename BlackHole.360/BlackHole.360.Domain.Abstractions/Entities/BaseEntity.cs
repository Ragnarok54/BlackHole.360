using BlackHole._360.Domain.Abstractions.Interfaces;

namespace BlackHole._360.Domain.Abstractions.Entities;

public abstract class BaseEntity : IIdentifiable, ITimeTracked, INameEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedOn { get;set; }
    public DateTime? ModifiedOn { get; set; }
}
