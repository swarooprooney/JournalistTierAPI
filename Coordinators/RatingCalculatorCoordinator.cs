using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JournalistTierAPI.Data;
using JournalistTierAPI.Dtos;
using JournalistTierAPI.Model;

namespace JournalistTierAPI.Coordinators
{
    public class RatingCalculatorCoordinator : IRatingCalculatorCoordinator
    {
        private readonly IMapper _mapper;
        private readonly IJournalistRepo _journalistRepo;
        private readonly IMediaRepo _mediaRepo;

        public RatingCalculatorCoordinator(IMapper mapper, IJournalistRepo journalistRepo, IMediaRepo mediaRepo)
        {
            _mapper = mapper;
            _journalistRepo = journalistRepo;
            _mediaRepo = mediaRepo;
        }

        public async Task<IEnumerable<RatingDto>> GetRatingByTopicAsync(int journalistId)
        {
            return await _journalistRepo.GetJournalistTopicRatingAsync(journalistId);
        }

        public async Task<double> GetRatingsAsync(TierQueryDto tierQueryDto)
        {
            double journalistRating = 0;
            double mediaRating = 0;
            if (tierQueryDto.JournalistId > 0)
            {
                journalistRating = await _journalistRepo.GetJournalistRatingAsync(_mapper.Map<UserJournalistRating>(tierQueryDto));
            }
            if (tierQueryDto.MediaId > 0)
            {
                mediaRating = await _mediaRepo.GetMediaRatingAsync(_mapper.Map<UserMediaRating>(tierQueryDto));
            }
            if (journalistRating > 0 && mediaRating > 0)
            {
                return (journalistRating + mediaRating) / 2;
            }
            else if (journalistRating > 0)
            {
                return journalistRating;
            }
            return mediaRating;
        }

        public async Task<int> GetTotalVotesAsync(TierQueryDto tierQueryDto)
        {
            int journalistVotes = 0;
            int mediaVotes = 0;
            if (tierQueryDto.JournalistId > 0)
            {
                journalistVotes = await _journalistRepo.GetTotalVotesAsync(tierQueryDto.JournalistId);
            }
            if (tierQueryDto.MediaId > 0)
            {
                mediaVotes = await _mediaRepo.GetTotalVotesAsync(tierQueryDto.MediaId);
            }
            return journalistVotes + mediaVotes;
        }
    }
}
