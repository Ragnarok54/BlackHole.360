using BlackHole._360.BusinessLogic.DTO.Generic;
using BlackHole._360.BusinessLogic.DTO.User;

namespace BlackHole._360.BusinessLogic.DTO.SubGroup;

public class SubGroupDto : BaseDto
{
    public Guid GroupId { get; set; }
    public IEnumerable<UserDto> Users { get; set; } = Enumerable.Empty<UserDto>();
}
