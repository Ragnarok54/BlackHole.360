using BlackHole._360.BusinessLogic.DTO.User;
using BlackHole._360.DataAccess.Abstractions;

namespace BlackHole._360.BusinessLogic.Services;

public class UserService(IUnitOfWork unitOfWork) : BaseService(unitOfWork)
{
    public async Task<UserDto> GetAsync(Guid id, CancellationToken cancellationToken = default) 
        => await UnitOfWork.UserRepository.GetAsync(id, cancellationToken) ?? throw new ArgumentException(null, nameof(id));

    public async Task<UserDto> GetByInternalIdAsync(Guid id, CancellationToken cancellationToken = default)
     => await UnitOfWork.UserRepository.FirstOrDefaultAsync(u => u.InternalId == id.ToString(), cancellationToken) ?? throw new ArgumentException(null, nameof(id));

    public async Task<Guid> GetIdByInternalAsync(Guid id, CancellationToken cancellationToken)
        => (await UnitOfWork.UserRepository.FirstOrDefaultAsync(u => u.InternalId == id.ToString(), cancellationToken))?.Id ?? throw new ArgumentException(null, nameof(id));

    public async Task<IEnumerable<UserDto>> GetAsync(string? search, int offset, int count, CancellationToken cancellationToken = default)
        => (await UnitOfWork.UserRepository.GetAsync(search, offset, count, cancellationToken)).Select(u => (UserDto)u);

    public async Task UpdateAsync(Guid id, UserDto userDto, CancellationToken cancellationToken = default)
    {
        var user = await UnitOfWork.UserRepository.FirstOrDefaultAsync(u => u.InternalId == id.ToString(), cancellationToken) ?? throw new ArgumentException(null, nameof(id));

        user.Name = userDto.Name;

        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
