using System.Linq.Expressions;

namespace BlackHole._360.DataAccess.Abstractions.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    TEntity? Get(Guid guid);
    Task<TEntity?> GetAsync(Guid guid, CancellationToken cancellationToken = default);

    IEnumerable<TEntity> GetAll();
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> GetAllWithoutQueryFiltersAsync();
    IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
    TEntity? SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    void Add(TEntity entity);
    Task AddAsync(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
}
