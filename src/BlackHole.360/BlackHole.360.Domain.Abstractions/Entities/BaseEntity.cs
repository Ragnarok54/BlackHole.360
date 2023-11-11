using BlackHole._360.Domain.Abstractions.Interfaces;

namespace BlackHole._360.Domain.Abstractions.Entities;

public abstract class BaseEntity : IIdentifiable, ITimeTracked
{
    public Guid Id { get; set; }
    public DateTime CreatedOn { get;set; }
    public DateTime? ModifiedOn { get; set; }
}
