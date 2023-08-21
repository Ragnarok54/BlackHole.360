namespace BlackHole._360.Domain.Interfaces.Entities;

public interface ITimeTracked
{
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}
