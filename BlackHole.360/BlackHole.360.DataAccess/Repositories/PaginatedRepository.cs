using BlackHole._360.DataAccess.Abstractions.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BlackHole._360.DataAccess.Repositories;

internal class PaginatedRepository<TEntity> : Repository<TEntity>, IPaginatedRepository<TEntity> where TEntity : class
{
    public PaginatedRepository(BlackHoleContext context) : base(context) { }


    public async Task<IEnumerable<TEntity>> GetAsync(int offset, int count, CancellationToken cancellationToken = default)
        => await _context.Set<TEntity>().Skip(offset).Take(count).ToListAsync(cancellationToken) ?? Enumerable.Empty<TEntity>();
}