using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JournalistTierAPI.Data;
using JournalistTierAPI.Dtos;
using JournalistTierAPI.Model;
using JournalistTierAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JournalistTierAPI.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUserRepo _repo;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IPhotoService _photoService;

        public AccountController(IUserRepo repo, IMapper mapper, ITokenService tokenService, IPhotoService photoService)
        {
            _tokenService = tokenService;
            _photoService = photoService;
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
                PasswordSalt = hmac.Key,
                City = userForRegisterDto.City,
                DateOfBirth = userForRegisterDto.DateOfBirth,
                Country = userForRegisterDto.Country,
                State = userForRegisterDto.State,
                KnownAs = userForRegisterDto.KnownAs
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

        [HttpPut("updateuser")]
        [Authorize]
        public async Task<ActionResult> UpdateUser(UserForUpdateDto userDto)
        {
            var user = await _repo.GetUserAsync(userDto.UserName);
            _mapper.Map(userDto, user);
            var isUpdated = await _repo.UpdateUserAsync(user);
            if (isUpdated)
            {
                return NoContent();
            }
            return BadRequest("Unable to update");
        }

        [HttpPost("add-photo")]
        [Authorize]
        public async Task<ActionResult> AddPhoto(IFormFile file)
        {
            if (file.Length > 0)
            {
                var uploadPhoto = await _photoService.AddPhotoAsync(file);
                return Ok(uploadPhoto.Url.ToString());
            }

            return BadRequest("Unable to update photo");
        }


        [HttpGet("{id}", Name = "GetUserByUserId")]
        public async Task<ActionResult<UserDto>> GetUserByUserId(int id)
        {
            var user = await _repo.GetUserAsync(id);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest($"Unable to find the user with userId : {id}");
        }
    }
}