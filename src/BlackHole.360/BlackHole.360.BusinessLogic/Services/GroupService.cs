﻿using BlackHole._360.BusinessLogic.DTO.Group;
using BlackHole._360.DataAccess.Abstractions;

namespace BlackHole._360.BusinessLogic.Services;

public class GroupService(IUnitOfWork unitOfWork) : BaseService(unitOfWork)
{
    public async Task<IEnumerable<GroupDto>> GetAllAsync(CancellationToken cancellationToken = default)
        => (await UnitOfWork.GroupRepository.GetAllAsync(cancellationToken)).Select(g => (GroupDto)g);
}
