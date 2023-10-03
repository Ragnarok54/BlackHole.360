using BlackHole._360.Domain.Abstractions.Entities;

namespace BlackHole._360.Domain.Entities;

public class Department : BaseEntity
{

    public virtual ICollection<Group> Groups { get; set; }
}
