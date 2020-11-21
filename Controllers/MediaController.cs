using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JournalistTierAPI.Data;
using JournalistTierAPI.Dtos;
using JournalistTierAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace JournalistTierAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediaRepo _mediaRepo;
        public MediaController(IMapper mapper, IMediaRepo mediaRepo)
        {
            _mediaRepo = mediaRepo;
            _mapper = mapper;

        }

        [HttpGet("{id}", Name = "GetMediaById")]
        public async Task<IActionResult> GetMediaById(int id)
        {
            if (id > 0)
            {
                var media = await _mediaRepo.GetMediaByIdAsync(id);
                return Ok(_mapper.Map<MediaDto>(media));
            }
            return NotFound("This is not a valid id");
        }

        [HttpPost]
        public async Task<IActionResult> AddMedia([FromBody] string mediaName)
        {
            if (!string.IsNullOrEmpty(mediaName))
            {
                var media = new Media { Name = mediaName };
                var isMediaAdded = await _mediaRepo.AddMediaAsync(media);
                if (isMediaAdded)
                {
                    return CreatedAtRoute("GetMediaById", new { id = media.MediaId }, _mapper.Map<MediaDto>(media));
                }
                return StatusCode(500, "Unable to Add Media at this time, please try later");
            }
            return BadRequest("The media name cannot be empty or null");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMedias()
        {
            var medias = await _mediaRepo.GetAllMediaAsync();
            return Ok(_mapper.Map<IEnumerable<MediaDto>>(medias));
        }

        [HttpPost("RateMedia")]
        public async Task<IActionResult> RateMedia([FromBody] UserMediaRatingDto userMediaRatingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var userMediaRating = _mapper.Map<UserMediaRating>(userMediaRatingDto);
            var result = await _mediaRepo.RateMediaAsync(userMediaRating);
            if (result)
            {
                return Ok();
            }
            return StatusCode(500, "Unable to Rate Media at this time, please try later");
        }
    }
}