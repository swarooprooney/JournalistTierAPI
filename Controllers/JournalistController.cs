using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using JournalistTierAPI.Data;
using System.Threading.Tasks;
using JournalistTierAPI.Dtos;
using JournalistTierAPI.Model;
using System.Collections.Generic;
using JournalistTierAPI.Coordinators;
using Microsoft.AspNetCore.Authorization;

namespace JournalistTierAPI.Controllers
{
    public class JournalistController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IJournalistRepo _repo;
        private readonly IRatingCalculatorCoordinator _ratingCoordinator;
        public JournalistController(IMapper mapper, IJournalistRepo repo, IRatingCalculatorCoordinator ratingCalculatorCoordinator)
        {
            _repo = repo;
            _mapper = mapper;
            _ratingCoordinator = ratingCalculatorCoordinator;
        }

        [HttpGet("{id}", Name = "GetJournalistById")]
        public async Task<IActionResult> GetJournalistById(int id)
        {
            var result = await _repo.GetJournalistByIdAsync(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound($"The Journalist with id: {id} is not found, please use add journalist method to add first");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddJournalist([FromBody] CreateJournalistDto createJournalist)
        {

            var journalist = new Journalist { Name = createJournalist.Name, Description = createJournalist.Description, PhotoUrl = createJournalist.PhotoUrl };
            bool result = await _repo.AddJournalistAsync(journalist);
            if (result)
            {
                return CreatedAtRoute("GetJournalistById", new { id = journalist.JournalistId }, _mapper.Map<JournalistDto>(journalist));
            }
            return StatusCode(500, "Unable to Add Journalist at this time, please try later");

        }

        [HttpGet]
        public async Task<IActionResult> GetAllJournalist()
        {
            var result = await _repo.GetAllJournalistAsync();
            return Ok(result);
        }

        [HttpPost("RateJournalist")]
        [Authorize]
        public async Task<IActionResult> RateJournalist([FromBody] UserJournalistRatingDto userJournalistRatingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var userJournalistRating = _mapper.Map<UserJournalistRating>(userJournalistRatingDto);
            var result = await _repo.RateJournalistAsync(userJournalistRating);
            if (result)
            {
                return Ok();
            }
            return StatusCode(500, "Unable to Rate Journalist at this time, please try later");
        }

        [HttpGet("GetJournalistTier")]
        public async Task<ActionResult<ReturnRatingDto>> GetJournalistTier([FromQuery] JournalistTierQueryDto tierQueryDto)
        {
            if (tierQueryDto.JournalistId <= 0)
            {
                return BadRequest("Journalist details are required to get the ratings.");
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

        [HttpGet("GetJournalistRatingByTopic")]
        public async Task<IActionResult> GetJournalistRatingByTopic([FromQuery] int journalistId)
        {
            var result = await _ratingCoordinator.GetRatingByTopicAsync(journalistId);
            return Ok(result);
        }
    }
}