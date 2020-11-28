using System.Linq;
using System.Threading.Tasks;
using JournalistTierAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace JournalistTierAPI.Data
{
    public class UserRepo : IUserRepo
    {
        private readonly DataContext _context;
        public UserRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterAsync(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.User.AnyAsync(x => x.UserName == username);
        }

        public async Task<User> GetUserAsync(string username)
        {
            return await _context.User.SingleOrDefaultAsync(x => x.UserName == username);
        }
    }
}