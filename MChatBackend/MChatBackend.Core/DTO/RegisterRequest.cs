using System;
using System.Collections.Generic;
using System.Text;

namespace MChatBackend.Core.DTO
{
    public class RegisterRequest
    {
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }

    }
}
