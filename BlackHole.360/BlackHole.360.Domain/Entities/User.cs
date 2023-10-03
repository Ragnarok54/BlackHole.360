using BlackHole._360.Domain.Abstractions.Entities;
using BlackHole._360.Domain.Abstractions.Interfaces;

namespace BlackHole._360.Domain.Entities;

public class User : BaseEntity, ISoftDelete
{
    public Guid InternalId { get; set; }
    public required string Email { get; set; }
    public Enums.JobTitle JobTitleId { get; set; }
    public Guid? SubgroupId { get; set; }
    public bool Deleted { get; set; }
    public DateTime? DeletedAt { get; set; }


    public virtual SubGroup? SubGroup { get; set; }
    public virtual JobTitle JobTitle { get; set; }
}
