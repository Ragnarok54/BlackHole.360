using BlackHole._360.BusinessLogic.DTO.Generic;
using BlackHole._360.BusinessLogic.DTO.Group;

namespace BlackHole._360.BusinessLogic.DTO.Department;

public class DepartmentDto : BaseDto
{
    public IEnumerable<GroupDto> Groups { get; set; } = Enumerable.Empty<GroupDto>();
}
