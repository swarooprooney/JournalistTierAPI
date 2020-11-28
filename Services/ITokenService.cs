using JournalistTierAPI.Model;

namespace JournalistTierAPI.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}