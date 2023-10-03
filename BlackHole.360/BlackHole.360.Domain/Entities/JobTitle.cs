using BlackHole._360.Domain.Abstractions.Interfaces;

namespace BlackHole._360.Domain.Entities;

public class JobTitle : INameEntity
{
    public Enums.JobTitle Id { get; set; }
    public required string Name { get;set; }


    public virtual ICollection<User> Users { get; set; }
}
