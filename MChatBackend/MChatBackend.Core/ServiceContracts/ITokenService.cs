using MChatBackend.Core.Domain.IdentityEntities;

namespace MChatBackend.Core.ServiceContracts
{
    public interface ITokenService
    {
        Task<(string Token, DateTime Expiration)> CreateToken(ApplicationUser user);
    }
}