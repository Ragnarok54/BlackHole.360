using BlackHole._360.DataAccess.Abstractions.Repositories;
using BlackHole._360.Domain.Abstractions.Entities;

using Microsoft.EntityFrameworkCore;

namespace BlackHole._360.DataAccess.Repositories;

internal class PaginatedRepository<TEntity> : Repository<TEntity>, IPaginatedRepository<TEntity> where TEntity : BaseNamedEntity
{
    public PaginatedRepository(BlackHoleContext context) : base(context) { }


    public async Task<IEnumerable<TEntity>> GetAsync(string? search, int offset, int count, CancellationToken cancellationToken = default)
        => await _context.Set<TEntity>().Where(e => string.IsNullOrEmpty(search) || e.Name.Contains(search))
                                        .Skip(offset)
                                        .Take(count)
                                        .ToListAsync(cancellationToken) ?? Enumerable.Empty<TEntity>();
}