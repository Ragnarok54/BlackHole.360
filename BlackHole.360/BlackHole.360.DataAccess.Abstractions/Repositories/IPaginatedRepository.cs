namespace BlackHole._360.DataAccess.Abstractions.Repositories;

public interface IPaginatedRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAsync(int offset, int count, CancellationToken cancellationToken = default);
}
