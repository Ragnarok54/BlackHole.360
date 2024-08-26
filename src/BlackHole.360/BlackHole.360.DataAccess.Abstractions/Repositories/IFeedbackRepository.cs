using BlackHole._360.Domain.Entities;

namespace BlackHole._360.DataAccess.Abstractions.Repositories;

public interface IFeedbackRepository : IRepository<Feedback>
{
    public Task<IEnumerable<Feedback>> GetAddedAsync(Guid id, CancellationToken cancellationToken);
    public Task<IEnumerable<Feedback>> GetReceivedAsync(Guid id, CancellationToken cancellationToken);
    public Task<Feedback> GetWithUserAsync(Guid id, CancellationToken cancellationToken);
}
