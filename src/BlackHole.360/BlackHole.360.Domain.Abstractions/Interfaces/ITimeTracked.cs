namespace BlackHole._360.Domain.Abstractions.Interfaces;

public interface ITimeTracked
{
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}
