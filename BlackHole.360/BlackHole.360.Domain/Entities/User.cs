using BlackHole._360.Domain.Abstractions;
using BlackHole._360.Domain.Interfaces.Entities;

namespace BlackHole._360.Domain.Entities;

public class User : BaseEntity, ISoftDelete
{


    public bool Deleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
