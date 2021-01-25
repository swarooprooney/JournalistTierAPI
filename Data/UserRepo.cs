using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JournalistTierAPI.Dtos;
using JournalistTierAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace JournalistTierAPI.Data
{
    public class UserRepo : IUserRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepo(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
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
        public async Task<UserDto> GetUserAsync(int id)
        {
            return await _context.User.ProjectTo<UserDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _context.User.Update(user);
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
    }
}