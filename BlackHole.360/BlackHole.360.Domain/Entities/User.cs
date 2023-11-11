using BlackHole._360.Domain.Abstractions.Entities;
using BlackHole._360.Domain.Abstractions.Interfaces;

namespace BlackHole._360.Domain.Entities;

public class User : BaseNamedEntity, ISoftDelete
{
    public string InternalId { get; set; }
    public required string Email { get; set; }
    public Enums.JobTitle JobTitleId { get; set; }
    public Guid? SubgroupId { get; set; }
    public bool Deleted { get; set; }
    public DateTime? DeletedAt { get; set; }


    public SubGroup? SubGroup { get; set; }
    public JobTitle JobTitle { get; set; }
    public ICollection<Feedback> GivenFeedback { get; set; } = new HashSet<Feedback>();
    public ICollection<Feedback> ReceievdFeedback { get; set; } = new HashSet<Feedback>();
}
