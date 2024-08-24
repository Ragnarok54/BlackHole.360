using BlackHole._360.DataAccess.Abstractions;
using BlackHole._360.DataAccess.Abstractions.Repositories;
using BlackHole._360.Domain.Entities;

namespace BlackHole._360.UnitTests.Repositories;

public class TestUnitOfWork : IUnitOfWork
{
    private bool isDisposed;

    IPaginatedRepository<User> IUnitOfWork.UserRepository => throw new NotImplementedException();

    public IRepository<Department> DepartmentRepository => throw new NotImplementedException();

    public IRepository<Group> GroupRepository => throw new NotImplementedException();

    public ILocalRepository<SubGroup> SubGroupRepository => throw new NotImplementedException();

    public IFeedbackRepository FeedbackRepository => throw new NotImplementedException();

    protected virtual void Dispose(bool disposing)
    {
        if (!isDisposed)
        {
            isDisposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public int SaveChanges() => 0;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
