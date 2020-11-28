using System.Threading.Tasks;
using JournalistTierAPI.Model;

namespace JournalistTierAPI.Data
{
    public interface IUserRepo
    {
        Task<User> RegisterAsync(User user);

        Task<bool> UserExistsAsync(string username);

        Task<User> GetUserAsync(string username);
    }
}