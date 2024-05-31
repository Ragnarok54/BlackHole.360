using BlackHole._360.BusinessLogic.DTO.Feedback;
using BlackHole._360.BusinessLogic.Services;

using Microsoft.AspNetCore.Mvc;

namespace BlackHole._360.Api.Controllers;

public class FeedbackController(FeedbackService feedbackService) : BaseController()
{
    [HttpGet]
    public async Task<ActionResult<FeedbackDto>> IndexAsync(CancellationToken cancellationToken = default)
        => Ok(await feedbackService.GetAddedAsync(CurrentUserId, cancellationToken));

    [HttpGet("received")]
    public async Task<ActionResult<FeedbackReceivedDto>> ReceievdAsync(CancellationToken cancellationToken = default)
        => Ok(await feedbackService.GetReceivedAsync(CurrentUserId, cancellationToken));

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] FeedbackEditDto feedback, CancellationToken cancellationToken = default)
        => CreatedAtAction(nameof(IndexAsync), await feedbackService.AddAsync(feedback, CurrentUserId, cancellationToken));

    [HttpPatch("{feebackId}")]
    public async Task<IActionResult> UpdateAsync(Guid feebackId, [FromBody] string content, CancellationToken cancellationToken = default)
    {
        if (await feedbackService.BelongsToUserAsync(feebackId, CurrentUserId, cancellationToken))
        {
            await feedbackService.UpdateAsync(feebackId, content, cancellationToken);

            return NoContent();
        }
        else
        {
            return Forbid();
        }
    }

    [HttpDelete("{feebackId}")]
    public async Task<IActionResult> DeleteAsync(Guid feebackId, CancellationToken cancellationToken = default)
    {
        if (await feedbackService.BelongsToUserAsync(feebackId, CurrentUserId, cancellationToken))
        {
            await feedbackService.DeleteAsync(feebackId, cancellationToken);

            return Ok();
        }
        else
        {
            return Forbid();
        }
    }
}
