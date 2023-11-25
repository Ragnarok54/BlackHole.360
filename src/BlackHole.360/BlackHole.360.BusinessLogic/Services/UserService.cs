using BlackHole._360.BusinessLogic.DTO.User;
using BlackHole._360.DataAccess.Abstractions;

namespace BlackHole._360.BusinessLogic.Services;
public class UserService(IUnitOfWork unitOfWork) : BaseService(unitOfWork)
{
    public async Task<UserDto> GetAsync(Guid id, CancellationToken cancellationToken = default) 
        => await UnitOfWork.UserRepository.GetAsync(id, cancellationToken) ?? throw new ArgumentException(null, nameof(id));

    public async Task<IEnumerable<UserDto>> GetAsync(string search, int offset, int count, CancellationToken cancellationToken = default)
        => (await UnitOfWork.UserRepository.GetAsync(search, offset, count, cancellationToken)).Select(u => (UserDto)u);
}
