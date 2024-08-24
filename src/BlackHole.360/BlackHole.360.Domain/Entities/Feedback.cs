using BlackHole._360.Domain.Abstractions.Entities;
using BlackHole._360.Domain.Abstractions.Interfaces;

namespace BlackHole._360.Domain.Entities;

public class Feedback : BaseEntity, ISoftDelete
{
    public Guid ToUserId { get; set; }
    public Guid? FromUserId { get; set; }
    public string Content {  get; set; } = string.Empty;
    public bool Deleted { get; set; }
    public DateTime? DeletedAt { get; set; }


    public User FromUser { get; set; } = null!;
    public User ToUser { get; set; } = null!;
}
