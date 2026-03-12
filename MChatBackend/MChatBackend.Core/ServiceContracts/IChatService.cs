using System;
using System.Collections.Generic;
using System.Text;

namespace MChatBackend.Core.ServiceContracts
{
    public interface IChatService
    {
        Task SendMessageAsync(string senderId, string receiverId, string message);
    }
}
