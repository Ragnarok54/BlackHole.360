namespace BlackHole._360.Domain.Interfaces.Entities;

public interface ISoftDelete
{
    public bool Deleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
