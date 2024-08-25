﻿using BlackHole._360.BusinessLogic.DTO.User;
using BlackHole._360.BusinessLogic.Services;

using Microsoft.AspNetCore.Mvc;

namespace BlackHole._360.Api.Controllers;

public class UsersController(UserService userService) : BaseController()
{
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> IndexAsync(Guid id, CancellationToken cancellationToken = default)
        => Ok(await userService.GetAsync(id, cancellationToken));

    [HttpGet("internal/{id}")]
    public async Task<ActionResult<UserDto>> InternalIdAsync(Guid id, CancellationToken cancellationToken = default)
    => Ok(await userService.GetByInternalIdAsync(id, cancellationToken));

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> IndexAsync([FromQuery]string? search, [FromQuery]int offset, [FromQuery]int count, CancellationToken cancellationToken)
        => Ok(await userService.GetAsync(search, offset, count, cancellationToken));


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProfileAsync(Guid id, [FromBody]UserDto user, CancellationToken cancellationToken = default)
    {
        await userService.UpdateAsync(id, user, cancellationToken);
        
        return NoContent();
    }
}
