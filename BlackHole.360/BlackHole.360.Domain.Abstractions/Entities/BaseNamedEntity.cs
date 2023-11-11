using BlackHole._360.Domain.Abstractions.Interfaces;

namespace BlackHole._360.Domain.Abstractions.Entities;

public abstract class BaseNamedEntity : BaseEntity, INameEntity
{
    public string Name { get; set; } = string.Empty;
}
