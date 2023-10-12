using BlackHole._360.DataAccess.Abstractions.Repositories;
using BlackHole._360.Domain.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlackHole._360.DataAccess.Repositories;

internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private protected readonly BlackHoleContext _context;

    public Repository(BlackHoleContext context)
    {
        _context = context;
    }

    public TEntity? Get(int id) 
        => _context.Set<TEntity>().Find(id);

    public TEntity? Get(Guid guid)
        => _context.Set<TEntity>().Find(guid);

    public async Task<TEntity?> GetAsync(Guid guid, CancellationToken cancellationToken = default)
        => await _context.Set<TEntity>().FindAsync(new object?[] { guid }, cancellationToken: cancellationToken);

    public IEnumerable<TEntity> GetAll()
        => _context.Set<TEntity>().ToList();

    public async Task<IEnumerable<TEntity>> GetAllAsync()
        => await _context.Set<TEntity>().ToListAsync();

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        => await _context.Set<TEntity>().ToListAsync(cancellationToken);

    public async Task<IEnumerable<TEntity>> GetAllWithoutQueryFiltersAsync()
        => await _context.Set<TEntity>().IgnoreQueryFilters().ToListAsync();

    public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        => _context.Set<TEntity>().Where(predicate);

    public TEntity? SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        => _context.Set<TEntity>().SingleOrDefault(predicate);

    public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        => await _context.Set<TEntity>().SingleOrDefaultAsync(predicate);


    public virtual void Add(TEntity entity)
        => _context.Set<TEntity>().Add(entity);

    public virtual async Task AddAsync(TEntity entity)
        => await _context.Set<TEntity>().AddAsync(entity);

    public virtual void AddRange(IEnumerable<TEntity> entities) 
        => _context.Set<TEntity>().AddRange(entities);

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        => await _context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);

    public void Remove(TEntity entity)
    {
        if (entity is ISoftDelete softDeleteEntity)
        {
            softDeleteEntity.Deleted = true;
            softDeleteEntity.DeletedAt = DateTime.UtcNow;
        }
        else
        {
            _context.Set<TEntity>().Remove(entity);
        }
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        if (entities.Any(e => e is ISoftDelete))
        {
            entities.ToList().ForEach(Remove);
        }
        else
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }
    }
}