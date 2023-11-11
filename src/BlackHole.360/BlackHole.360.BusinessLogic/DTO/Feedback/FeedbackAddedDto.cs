namespace BlackHole._360.BusinessLogic.DTO.Feedback;

public class FeedbackAddedDto : FeedbackDto
{
    public required string ToUser { get; set; }


    public static implicit operator FeedbackAddedDto(Domain.Entities.Feedback feedback)
    => new()
    {
        Id = feedback.Id,
        Content = feedback.Content,
        ToUser = feedback.ToUser.Name,
        Name = string.Empty,
    };
}
