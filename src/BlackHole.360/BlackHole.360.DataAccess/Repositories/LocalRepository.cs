using BlackHole._360.DataAccess.Abstractions.Repositories;

namespace BlackHole._360.DataAccess.Repositories;

internal class LocalRepository<TEntity> : Repository<TEntity>, ILocalRepository<TEntity> where TEntity : class
{
    public LocalRepository(BlackHoleContext context) : base(context) { }

    public IEnumerable<TEntity> GetLocal(Func<TEntity, bool> predicate, CancellationToken cancellationToken = default)
        => _context.Set<TEntity>().Local.Where(predicate).ToList();
}
