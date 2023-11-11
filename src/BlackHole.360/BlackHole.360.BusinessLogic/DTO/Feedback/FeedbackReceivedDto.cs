namespace BlackHole._360.BusinessLogic.DTO.Feedback;

public class FeedbackReceivedDto : FeedbackDto
{
    public required string FromUser { get; set; }

    public static implicit operator FeedbackReceivedDto(Domain.Entities.Feedback feedback)
        => new()
        {
            Id = feedback.Id,
            Content = feedback.Content,
            FromUser = feedback.FromUser.Name,
            Name = string.Empty,
        };

}
