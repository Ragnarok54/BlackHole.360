using BlackHole._360.DataAccess.Abstractions;
using BlackHole._360.DataAccess.Abstractions.Repositories;
using BlackHole._360.Domain.Abstractions.Interfaces;
using BlackHole._360.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlackHole._360.DataAccess;
public class UnitOfWork(IServiceProvider serviceProvider, BlackHoleContext context) : IUnitOfWork
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly BlackHoleContext _context = context;

    private IPaginatedRepository<User> _userRepository = null!;
    private IRepository<Department> _departmentRepository = null!;
    private IRepository<Group> _groupRepository = null!;
    private ILocalRepository<SubGroup> _subGroupRepository = null!;
    private IFeedbackRepository _feedbackRepository = null!;


    public IPaginatedRepository<User> UserRepository => InitService(ref _userRepository);
    public IRepository<Department> DepartmentRepository => InitService(ref _departmentRepository);
    public IRepository<Group> GroupRepository => InitService(ref _groupRepository);
    public ILocalRepository<SubGroup> SubGroupRepository => InitService(ref _subGroupRepository);
    public IFeedbackRepository FeedbackRepository => InitService(ref _feedbackRepository);

    public int SaveChanges()
    {
        try
        {
            PerformExtraOperations();

            return _context.SaveChanges();
        }
        catch (Exception ex)
        {
            // TODO: Log ex
            throw ex;
        }
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        try
        {
            PerformExtraOperations();

            return await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            // TODO: Log ex
            throw ex;
        }
    }

    private T InitService<T>(ref T service)
        => service ??= _serviceProvider.GetService<T>() ?? throw new ArgumentNullException(nameof(service), "Service has not been configured correctly for DI");

    private void PerformExtraOperations()
    {
        var entries = _context.ChangeTracker.Entries<ITimeTracked>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.ModifiedOn = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedOn = DateTime.UtcNow;
            }
        }
    }

    #region Implementation of IDisposable
    private bool isDisposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!isDisposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            isDisposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion
}
