using BlackHole._360.BusinessLogic.DTO.Generic;

namespace BlackHole._360.BusinessLogic.DTO.Feedback;

public class FeedbackDto : IdentifiableDto
{
    public required string Content { get; set; }
    public DateTime CreatedOn { get; set; }


    public static implicit operator FeedbackDto(Domain.Entities.Feedback feedback)
        => new()
        {
            Id = feedback.Id,
            Content = feedback.Content,
            CreatedOn = feedback.CreatedOn
        };
}
