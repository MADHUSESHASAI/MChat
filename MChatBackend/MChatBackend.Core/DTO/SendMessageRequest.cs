using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MChatBackend.Core.DTO
{
    public class SendMessageRequest
    {
        [Required]
        public string? SenderId { get; set; }
        [Required]
        public string? ReceiverId { get; set; }
        [Required]
        [MaxLength(2000)]
        public string? Message { get; set; }
    }
}
