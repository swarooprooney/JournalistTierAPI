using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JournalistTierAPI.Data;
using JournalistTierAPI.Dtos;
using JournalistTierAPI.Model;
using JournalistTierAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace JournalistTierAPI.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUserRepo _repo;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public AccountController(IUserRepo repo, IMapper mapper, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(UserForRegisterDto userForRegisterDto)
        {
            if (await _repo.UserExistsAsync(userForRegisterDto.UserName))
            {
                return BadRequest("Username is taken");
            }
            using var hmac = new HMACSHA512();

            var user = new User
            {
                UserName = userForRegisterDto.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userForRegisterDto.Password)),
                PasswordSalt = hmac.Key
            };
            var registeredUser = await _repo.RegisterAsync(user);
            var userDto = _mapper.Map<UserDto>(registeredUser);
            userDto.Token = _tokenService.CreateToken(registeredUser);
            return Ok(userDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _repo.GetUserAsync(loginDto.UserName);
            if (user == null)
            {
                return Unauthorized("Invalid Username");
            }
            var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Invalid Password");
                }
            }
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = _tokenService.CreateToken(user);
            return Ok(userDto);
        }
    }
}