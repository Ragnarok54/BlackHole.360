using BlackHole._360.BusinessLogic.DTO.Feedback;
using BlackHole._360.DataAccess.Abstractions;

namespace BlackHole._360.BusinessLogic.Services;
public class FeedbackService(IUnitOfWork unitOfWork) : BaseService(unitOfWork)
{
    public async Task<IEnumerable<FeedbackDto>> GetAddedAsync(Guid userId, CancellationToken cancellationToken)
        => (await UnitOfWork.FeedbackRepository.GetAddedAsync(userId, cancellationToken)).Select(f => (FeedbackAddedDto)f);

    public async Task<IEnumerable<FeedbackReceivedDto>> GetReceivedAsync(Guid userId, CancellationToken cancellationToken)
        => (await UnitOfWork.FeedbackRepository.GetReceivedAsync(userId, cancellationToken)).Select(f => (FeedbackReceivedDto)f);

    public async Task<FeedbackAddedDto> AddAsync(FeedbackEditDto feedbackDto, Guid currentUserId, CancellationToken cancellationToken)
    {
        var feedback = new Domain.Entities.Feedback
        {
            FromUserId = feedbackDto.IsAnonymous ? null : currentUserId,
            ToUserId = feedbackDto.ToUserId,
            Content = feedbackDto.Content,
        };

        await UnitOfWork.FeedbackRepository.AddAsync(feedback, cancellationToken);

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return feedback;
    }

    public async Task UpdateAsync(Guid id, string content, CancellationToken cancellationToken)
    {
        var feedback = await UnitOfWork.FeedbackRepository.GetAsync(id, cancellationToken) ?? throw new ArgumentException(null, nameof(id));

        feedback.Content = content;

        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task MakeAnonymousAsync(Guid id, CancellationToken cancellationToken)
    {
        var feedback = await UnitOfWork.FeedbackRepository.GetAsync(id, cancellationToken) ?? throw new ArgumentException(null, nameof(id));

        feedback.FromUserId = null;
        
        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var feedback = await UnitOfWork.FeedbackRepository.GetAsync(id, cancellationToken) ?? throw new ArgumentException(null, nameof(id));

        UnitOfWork.FeedbackRepository.Remove(feedback);

        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> BelongsToUserAsync(Guid userId, Guid feedbackId, CancellationToken cancellationToken)
        => (await UnitOfWork.FeedbackRepository.GetAsync(feedbackId, cancellationToken))?.FromUserId == userId;
}
