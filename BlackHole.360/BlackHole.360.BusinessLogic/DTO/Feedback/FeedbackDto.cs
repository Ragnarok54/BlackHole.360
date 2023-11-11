using BlackHole._360.BusinessLogic.DTO.Generic;

namespace BlackHole._360.BusinessLogic.DTO.Feedback;

public class FeedbackDto : BaseDto
{
    public required string Content { get; set; }


    public static implicit operator FeedbackDto(Domain.Entities.Feedback feedback)
        => new()
        {
            Id = feedback.Id,
            Content = feedback.Content,
            Name = string.Empty,
        };
}
