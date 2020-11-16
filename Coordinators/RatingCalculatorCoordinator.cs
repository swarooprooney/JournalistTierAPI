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
            if (journalistRating > mediaRating)
            {
                return journalistRating;
            }
            return mediaRating;
        }
    }
}
