using BlackHole._360.DataAccess.Abstractions.Repositories;
using BlackHole._360.Domain.Entities;

namespace BlackHole._360.DataAccess.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IPaginatedRepository<User> UserRepository { get; }
    IRepository<Department> DepartmentRepository { get; }
    IRepository<Group> GroupRepository { get; }
    ILocalRepository<SubGroup> SubGroupRepository { get; }
    IFeedbackRepository FeedbackRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
