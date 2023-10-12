using BlackHole._360.BusinessLogic.DTO.User;
using BlackHole._360.BusinessLogic.Services;

using Microsoft.AspNetCore.Mvc;

namespace BlackHole._360.Api.Controllers;

public class UsersController : BaseController
{
    private readonly UserService _userService;

    public UsersController(UserService userService) : base() 
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> IndexAsync(Guid id, CancellationToken cancellationToken = default)
        => Ok(await _userService.GetAsync(id, cancellationToken));

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> IndexAsync([FromQuery]int offset, [FromQuery]int count, CancellationToken cancellationToken)
        => Ok(await _userService.GetAsync(offset, count, cancellationToken));
}
