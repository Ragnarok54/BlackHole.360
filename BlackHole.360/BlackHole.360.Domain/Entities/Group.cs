using BlackHole._360.Domain.Abstractions.Entities;

namespace BlackHole._360.Domain.Entities;

public class Group : BaseEntity
{
    public Guid DepartmentId { get; set; }


    public virtual Department Department { get; set; }
    public virtual ICollection<SubGroup> SubGroups { get; set; }
}
