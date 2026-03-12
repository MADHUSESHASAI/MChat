using MChatBackend.Core.DTO;
using MChatBackend.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]/[action]")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
    {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }
        await _chatService.SendMessageAsync(
            request.SenderId!,
            request.ReceiverId!,
            request.Message!);

        return Ok(new { message = "Message sent successfully" });
    }
}