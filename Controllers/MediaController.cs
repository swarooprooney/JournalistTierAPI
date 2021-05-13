using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JournalistTierAPI.Coordinators;
using JournalistTierAPI.Data;
using JournalistTierAPI.Dtos;
using JournalistTierAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JournalistTierAPI.Controllers
{
    public class MediaController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IMediaRepo _mediaRepo;
        private readonly IRatingCalculatorCoordinator _ratingCoordinator;
        public MediaController(IMapper mapper, IMediaRepo mediaRepo, IRatingCalculatorCoordinator ratingCoordinator = null)
        {
            _mediaRepo = mediaRepo;
            _mapper = mapper;
            _ratingCoordinator = ratingCoordinator;
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
        [Authorize]
        public async Task<IActionResult> AddMedia([FromBody] MediaDto media)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var isMediaAdded = await _mediaRepo.AddMediaAsync(_mapper.Map<Media>(media));
            if (isMediaAdded)
            {
                return CreatedAtRoute("GetMediaById", new { id = media.MediaId }, _mapper.Map<MediaDto>(media));
            }
            return StatusCode(500, "Unable to Add Media at this time, please try later");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMedias()
        {
            var medias = await _mediaRepo.GetAllMediaAsync();
            return Ok(_mapper.Map<IEnumerable<MediaDto>>(medias));
        }

        [HttpPost("RateMedia")]
        [Authorize]
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

        [HttpGet("GetMediaRating")]
        public async Task<IActionResult> GetMediaRating([FromQuery] MediaTierQueryDto tierQueryDto)
        {
            if (tierQueryDto.MediaId <= 0)
            {
                return BadRequest("Media information is requried to provide the rating");
            }
            var retunrRatingDto = new ReturnRatingDto();
            retunrRatingDto.Rating = await _ratingCoordinator.GetRatingsAsync(_mapper.Map<TierQueryDto>(tierQueryDto));
            if (retunrRatingDto.Rating <= 0)
            {
                retunrRatingDto.Rating = 0.0d;
            }
            retunrRatingDto.NumberOfVotes = await _ratingCoordinator.GetTotalVotesAsync(_mapper.Map<TierQueryDto>(tierQueryDto));
            return Ok(retunrRatingDto);
        }
    }
}