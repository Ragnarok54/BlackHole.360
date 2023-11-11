using BlackHole._360.Domain.Abstractions.Entities;

namespace BlackHole._360.Domain.Entities;

public class SubGroup : BaseNamedEntity
{
    public Guid GroupId { get; set; }


    public Group Group { get; set; }
    public ICollection<User> Users { get; set; }
}
