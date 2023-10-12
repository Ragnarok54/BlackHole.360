using BlackHole._360.DataAccess.Abstractions;
using BlackHole._360.Domain.Entities;
using System.Text.Json;

namespace BlackHole._360.BusinessLogic.Services;
public class ImportService : BaseService
{
    public ImportService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    public async Task ImportUsersAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        var importList = await JsonSerializer.DeserializeAsync<IEnumerable<User>>(stream, cancellationToken: cancellationToken) ?? Enumerable.Empty<User>();

        await ImportUsersAsync(importList);
    }

    public async Task ImportUsersAsync(IEnumerable<User> users)
    {
        await UnitOfWork.UserRepository.AddRangeAsync(users);

        await UnitOfWork.SaveChangesAsync();
    }

}
