using BlackHole._360.Domain.Abstractions.Entities;

namespace BlackHole._360.Domain.Entities;

public class Group : BaseNamedEntity
{
    public Guid DepartmentId { get; set; }


    public Department Department { get; set; }
    public ICollection<SubGroup> SubGroups { get; set; } = new HashSet<SubGroup>();
}
