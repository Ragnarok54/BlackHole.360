using BlackHole._360.BusinessLogic.DTO.Generic;

namespace BlackHole._360.BusinessLogic.DTO.Group;

public class GroupDto : BaseDto
{
    public Guid DepartmentId { get; set; }
    public IEnumerable<SubGroupDto> SubGroups { get; set; } = Enumerable.Empty<SubGroupDto>();

    public static implicit operator GroupDto(Domain.Entities.Group group)
        => new()
        {
            Id = group.Id,
            Name = group.Name,
            DepartmentId = group.DepartmentId,
        };
}
