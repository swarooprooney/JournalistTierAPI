using System.Collections.Generic;
using System.Threading.Tasks;
using JournalistTierAPI.Dtos;

namespace JournalistTierAPI.Coordinators
{
    public interface IRatingCalculatorCoordinator
    {
        Task<double> GetRatingsAsync(TierQueryDto tierQueryDto);

        Task<IEnumerable<RatingDto>> GetRatingByTopicAsync(int JournalistId);

        Task<int> GetTotalVotesAsync(TierQueryDto tierQueryDto);
    }
}
