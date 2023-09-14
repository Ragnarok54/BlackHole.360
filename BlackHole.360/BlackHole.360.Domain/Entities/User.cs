using BlackHole._360.Domain.Abstractions.Entities;
using BlackHole._360.Domain.Abstractions.Interfaces;

namespace BlackHole._360.Domain.Entities;

public class User : BaseEntity, ISoftDelete
{
    public bool Deleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
