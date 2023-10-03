using BlackHole._360.Domain.Abstractions.Entities;

namespace BlackHole._360.Domain.Entities;

public class SubGroup : BaseEntity
{
    public Guid GroupId { get; set; }


    public virtual Group Group { get; set; }
    public virtual ICollection<User> Users { get; set; }
}
