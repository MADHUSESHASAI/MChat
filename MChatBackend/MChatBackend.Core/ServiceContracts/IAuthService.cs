using MChatBackend.Core.DTO;
using MChatBackend.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MChatBackend.Core.ServiceContracts
{
    public interface IAuthService
    {
        Task<AuthenticationResponse?> RegisterService(RegisterRequest req);
        Task<AuthenticationResponse?> Login(LoginRequest req);

        Task<bool> Logout();

    }
}
