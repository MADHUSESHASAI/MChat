using Microsoft.AspNetCore.SignalR;

using MChatBackend.Core.ServiceContracts;

namespace MChatBackend.Core.SignalR.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        /// <summary>
        /// User → User Message Sending
        /// </summary>
        public async Task SendMessage(
            string senderId,
            string receiverId,
            string message)
        {
            await _chatService.SendMessageAsync(
                senderId,
                receiverId,
                message);
        }
    }
}