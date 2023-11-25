using BlackHole._360.BusinessLogic.DTO.User;
using BlackHole._360.BusinessLogic.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace BlackHole._360.Api.Controllers;

public class GroupsController(GroupService groupService) : BaseController()
{
    [HttpGet]
    [OutputCache]
    public async Task<ActionResult<UserDto>> IndexAsync(CancellationToken cancellationToken = default)
        => Ok(await groupService.GetAllAsync(cancellationToken));
}
