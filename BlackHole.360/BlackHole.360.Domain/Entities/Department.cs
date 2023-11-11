using BlackHole._360.Domain.Abstractions.Entities;

namespace BlackHole._360.Domain.Entities;

public class Department : BaseNamedEntity
{
    public ICollection<Group> Groups { get; set; } = new HashSet<Group>();
}
