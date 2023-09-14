namespace BlackHole._360.Domain.Abstractions.Interfaces;

public interface ISoftDelete
{
    public bool Deleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
