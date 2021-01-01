using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JournalistTierAPI.Data;
using JournalistTierAPI.Dtos;
using JournalistTierAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JournalistTierAPI.Controllers
{
    public class TopicController : BaseApiController
    {
        private readonly ITopicRepo _topicRepo;
        private readonly IMapper _mapper;

        public TopicController(ITopicRepo topicRepo, IMapper mapper)
        {
            _mapper = mapper;
            _topicRepo = topicRepo;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddTopic([FromBody] string topicName)
        {
            if (!string.IsNullOrEmpty(topicName))
            {

                var topic = new Topic { Name = topicName };
                var result = await _topicRepo.InsertTopicToDatabaseAsync(topic);
                if (result)
                {
                    return CreatedAtRoute("GetTopicById", new { id = topic.TopicId }, _mapper.Map<TopicDto>(topic));
                }
                return StatusCode(500, "Unable to Add topic at this time, please try later");
            }
            return BadRequest("you must atleast have one topic to Add");
        }

        [HttpGet("{id}", Name = "GetTopicById")]
        public async Task<IActionResult> GetTopicById(int id)
        {
            var result = await _topicRepo.GetTopicFromDbByIdAsync(id);
            return Ok(_mapper.Map<TopicDto>(result));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTopics()
        {
            var result = await _topicRepo.GetTopicsAsync();
            return Ok(_mapper.Map<IEnumerable<TopicDto>>(result));
        }
    }
}