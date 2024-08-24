using BlackHole._360.DataAccess.Abstractions.Repositories;
using BlackHole._360.Domain.Abstractions.Entities;
using BlackHole._360.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlackHole._360.DataAccess.Repositories;

internal class PaginatedRepository<TEntity>(BlackHoleContext context) : Repository<TEntity>(context), IPaginatedRepository<TEntity> where TEntity : User
{
    public async Task<IEnumerable<TEntity>> GetAsync(string? search, int offset, int count, CancellationToken cancellationToken = default)
        => await _context.Set<TEntity>().Where(e => string.IsNullOrEmpty(search) || e.Name.Contains(search))
                                        //.Where(e => e.JobTitleId != Domain.Enums.JobTitle.Unknown)
                                        .OrderBy(e => e.JobTitleId == Domain.Enums.JobTitle.Student)
                                        .ThenBy(e => e.Name) 
                                        .Skip(offset)
                                        .Take(count)
                                        .ToListAsync(cancellationToken) ?? Enumerable.Empty<TEntity>();
}