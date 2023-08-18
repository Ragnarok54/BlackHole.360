using Microsoft.AspNetCore.Mvc;

namespace BlackHole._360.Api.Controllers;

public class UsersController : BaseController
{
    [HttpGet("{id}")]
    public async Task<IActionResult> IndexAsync(Guid id)
        => Ok(Task.CompletedTask);
}
