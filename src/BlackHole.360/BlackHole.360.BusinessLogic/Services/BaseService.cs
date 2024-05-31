using BlackHole._360.DataAccess.Abstractions;

namespace BlackHole._360.BusinessLogic.Services;

public abstract class BaseService
{
    private protected readonly IUnitOfWork UnitOfWork;

    private protected BaseService(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }
}