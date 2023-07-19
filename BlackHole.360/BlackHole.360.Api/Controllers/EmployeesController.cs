using Microsoft.AspNetCore.Mvc;

namespace BlackHole._360.Api.Controllers;

public class EmployeesController : BaseController
{
    [HttpGet("{id}")]
    public async Task<IActionResult> IndexAsync(Guid id)
        => Ok(Task.CompletedTask);
}
