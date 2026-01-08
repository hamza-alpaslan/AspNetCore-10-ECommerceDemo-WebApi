using Domain.Entities;

namespace Web.Security
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(ApplicationUser applicationUser);
        public Task<string> GenerateRefreshToken();
    }
}
