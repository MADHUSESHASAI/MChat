using MChatBackend.Core.Domain.IdentityEntities;
using MChatBackend.Core.DTO;
using MChatBackend.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MChatBackend.Core.ReposetryContracts
{
    public interface IAuthReposetry
    {
        Task<ApplicationUser?> Register(ApplicationUser user, string password);

        Task<ApplicationUser?> UserLogin(string username, string password);
        Task<bool> UserLogout();
    }
}
