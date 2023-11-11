namespace BlackHole._360.DataAccess.Abstractions.Repositories;

public interface ILocalRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    IEnumerable<TEntity> GetLocal(Func<TEntity, bool> predicate, CancellationToken cancellationToken = default);
}
