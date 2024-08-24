namespace BlackHole._360.BusinessLogic.DTO.Feedback;

public class FeedbackEditDto : FeedbackDto
{
    public Guid ToUserId { get; set; }
    public bool IsAnonymous { get; set; }
}
