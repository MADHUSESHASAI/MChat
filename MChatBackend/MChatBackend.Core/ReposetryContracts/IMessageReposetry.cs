using MChatBackend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MChatBackend.Core.ReposetryContracts
{
    public interface IMessageRepository
    {
        Task SaveMessageAsync(Message message);

        Task<List<Message>> GetChatHistoryAsync(
            string user1,
            string user2,
            int page,
            int pageSize);
    }
}
