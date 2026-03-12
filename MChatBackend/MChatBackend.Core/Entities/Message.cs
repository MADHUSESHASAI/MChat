using MChatBackend.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace MChatBackend.Core.Entities
{
    public class Message
    {
        public Guid Id { get; set; }

        // Sender & Receiver
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }

        // Message Content
        public string Content { get; set; }

        // Metadata
        public DateTime CreatedAt { get; set; }

        // Delivery Tracking ⭐ (Very Important for product apps)
        public MessageStatus Status { get; set; }

        // Optional Features
        public bool IsDeleted { get; set; }
    }
}
