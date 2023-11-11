using BlackHole._360.Domain.Entities;

namespace BlackHole._360.DataAccess.Abstractions.Repositories;

public interface IFeedbackRepository : IRepository<Feedback>
{
    public Task<IEnumerable<Feedback>> GetAddedAsync(Guid userId, CancellationToken cancellationToken);
    public Task<IEnumerable<Feedback>> GetReceivedAsync(Guid userId, CancellationToken cancellationToken);
}
