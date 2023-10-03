using BlackHole._360.DataAccess.Abstractions.Repositories;
using BlackHole._360.Domain.Entities;

namespace BlackHole._360.DataAccess.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> UserRepository { get; }


    int SaveChanges();
    Task<int> SaveChangesAsync();
}
