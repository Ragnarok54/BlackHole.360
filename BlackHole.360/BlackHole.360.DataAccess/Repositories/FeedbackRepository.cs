using BlackHole._360.DataAccess.Abstractions.Repositories;
using BlackHole._360.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace BlackHole._360.DataAccess.Repositories;

internal class FeedbackRepository : Repository<Feedback>, IFeedbackRepository
{
    public FeedbackRepository(BlackHoleContext context) : base(context) { }


    public async Task<IEnumerable<Feedback>> GetAddedAsync(Guid userId, CancellationToken cancellationToken)
        => await _context.Set<Feedback>().Where(f => f.FromUserId == userId)
                                         .Include(f => f.ToUser)
                                         .AsSplitQuery()
                                         .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Feedback>> GetReceivedAsync(Guid userId, CancellationToken cancellationToken)
        => await _context.Set<Feedback>().Include(f => f.FromUser)
                                         .Where(f => f.ToUserId == userId)
                                         .AsSplitQuery()
                                         .ToListAsync(cancellationToken);
}
