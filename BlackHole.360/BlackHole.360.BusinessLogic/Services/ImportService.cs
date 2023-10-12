using BlackHole._360.BusinessLogic.DTO.User;
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

        foreach (var importUser in importList) 
        {
            UnitOfWork.UserRepository.Add(importUser);
        }

        await UnitOfWork.SaveChangesAsync();
    }

}
