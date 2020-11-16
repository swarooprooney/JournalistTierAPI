using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using JournalistTierAPI.Data;
using System.Threading.Tasks;
using JournalistTierAPI.Dtos;
using JournalistTierAPI.Model;
using System.Collections.Generic;
using JournalistTierAPI.Coordinators;

namespace JournalistTierAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JournalistController : ControllerBase
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
                return Ok(_mapper.Map<JournalistDto>(result));
            }
            return NotFound($"The Journalist with id: {id} is not found, please use add journalist method to add first");
        }

        [HttpPost]
        public async Task<IActionResult> AddJournalist([FromBody] string journalistName)
        {
            if (!string.IsNullOrEmpty(journalistName))
            {
                var journalist = new Journalist { Name = journalistName };
                bool result = await _repo.AddJournalistAsync(journalist);
                if (result)
                {
                    return CreatedAtRoute("GetJournalistById", new { id = journalist.JournalistId }, _mapper.Map<JournalistDto>(journalist));
                }
                return StatusCode(500, "Unable to Add Journalist at this time, please try later");
            }

            return BadRequest("Journalist Name cannot be empty");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJournalist()
        {
            var result = await _repo.GetAllJournalistAsync();
            return Ok(_mapper.Map<IEnumerable<JournalistDto>>(result));
        }

        [HttpPost("RateJournalist")]
        public async Task<IActionResult> RateJournalist([FromBody] UserJournalistRatingDto userJournalistRatingDto)
        {
            var userJournalistRating = _mapper.Map<UserJournalistRating>(userJournalistRatingDto);
            var result = await _repo.RateJournalistAsync(userJournalistRating);
            if (result)
            {
                return Ok();
            }
            return StatusCode(500, "Unable to Rate Journalist at this time, please try later");
        }

        [HttpGet("GetJournalistTier")]
        public async Task<IActionResult> GetJournalistTier([FromQuery] TierQueryDto tierQueryDto)
        {
            if (tierQueryDto.JournalistId == 0 && tierQueryDto.MediaId == 0)
            {
                return BadRequest("You should atleast provide information about journalist or media you are trying to look up");
            }
            var result = await _ratingCoordinator.GetRatingsAsync(tierQueryDto);
            if (result <= 0)
            {
                return NotFound("Not ratings are recorded for given journalist/media");
            }
            return Ok(result);
        }

    }
}