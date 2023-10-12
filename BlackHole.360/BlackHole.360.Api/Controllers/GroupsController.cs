using BlackHole._360.BusinessLogic.DTO.User;
using BlackHole._360.BusinessLogic.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace BlackHole._360.Api.Controllers;

public class GroupsController : BaseController
{
    private readonly GroupService _groupService;

    public GroupsController(GroupService groupService) : base() 
    {
        _groupService = groupService;
    }


    [HttpGet]
    [OutputCache]
    public async Task<ActionResult<UserDto>> IndexAsync(CancellationToken cancellationToken = default)
        => Ok(await _groupService.GetAllAsync(cancellationToken));
}
