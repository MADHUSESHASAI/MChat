using MChatBackend.Core.Entities;
using MChatBackend.Core.Enum;
using MChatBackend.Core.ReposetryContracts;
using MChatBackend.Core.ServiceContracts;
using Microsoft.AspNetCore.SignalR;
using MChatBackend.Core.SignalR.Hubs;


namespace MChatBackend.Core.Services
{
    public class ChatService : IChatService
    {
        private readonly IMessageRepository _repository;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatService(
            IMessageRepository repository,
            IHubContext<ChatHub> hubContext)
        {
            _repository = repository;
            _hubContext = hubContext;
        }

        public async Task SendMessageAsync(
            string senderId,
            string receiverId,
            string message)
        {
            var chatMessage = new Message
            {
                Id = Guid.NewGuid(),
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = message,
                CreatedAt = DateTime.UtcNow,
                Status = MessageStatus.Sent
            };

            // ✅ Save message first (Database persistence)
            await _repository.SaveMessageAsync(chatMessage);

            // ✅ Real-time broadcast via SignalR
            await _hubContext.Clients.User(receiverId)
                .SendAsync("ReceiveMessage", chatMessage);
        }
    }
}
