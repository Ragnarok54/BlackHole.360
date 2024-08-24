using System.Linq.Expressions;
using System.Threading;

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
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    void Add(TEntity entity);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    void AddRange(IEnumerable<TEntity> entities);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
}
