using BlackHole._360.BusinessLogic.DTO.Feedback;
using BlackHole._360.BusinessLogic.Services;

using Microsoft.AspNetCore.Mvc;

namespace BlackHole._360.Api.Controllers;

public class FeedbackController : BaseController
{
    private readonly FeedbackService _feedbackService;

    public FeedbackController(FeedbackService feedbackService) : base()
    {
        _feedbackService = feedbackService;
    }

    [HttpGet]
    public async Task<ActionResult<FeedbackDto>> IndexAsync(CancellationToken cancellationToken = default)
        => Ok(await _feedbackService.GetAddedAsync(CurrentUserId, cancellationToken));

    [HttpGet("received")]
    public async Task<ActionResult<FeedbackReceivedDto>> ReceievdAsync(CancellationToken cancellationToken = default)
        => Ok(await _feedbackService.GetReceivedAsync(CurrentUserId, cancellationToken));

    [HttpPost]
    public async Task AddAsync([FromBody] FeedbackEditDto feedback, CancellationToken cancellationToken = default)
        => CreatedAtAction(nameof(IndexAsync), await _feedbackService.AddAsync(feedback, CurrentUserId, cancellationToken));

    [HttpPatch("{feebackId}")]
    public async Task<IActionResult> UpdateAsync(Guid feebackId, [FromBody] string content, CancellationToken cancellationToken = default)
    {
        if (await _feedbackService.BelongsToUserAsync(feebackId, CurrentUserId, cancellationToken))
        {
            await _feedbackService.UpdateAsync(feebackId, content, cancellationToken);

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
        if (await _feedbackService.BelongsToUserAsync(feebackId, CurrentUserId, cancellationToken))
        {
            await _feedbackService.DeleteAsync(feebackId, cancellationToken);

            return Ok();
        }
        else
        {
            return Forbid();
        }
    }
}
