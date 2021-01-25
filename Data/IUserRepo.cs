using System.Threading.Tasks;
using JournalistTierAPI.Dtos;
using JournalistTierAPI.Model;

namespace JournalistTierAPI.Data
{
    public interface IUserRepo
    {
        Task<User> RegisterAsync(User user);

        Task<bool> UserExistsAsync(string username);

        Task<User> GetUserAsync(string username);

        Task<UserDto> GetUserAsync(int id);

        Task<bool> UpdateUserAsync(User user);
    }
}