using BlackHole._360.Domain.Abstractions;
using BlackHole._360.Domain.Interfaces.Entities;

namespace BlackHole._360.Domain.Entities;

public class Employee : BaseEntity, ISoftDelete
{
    public bool Deleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
