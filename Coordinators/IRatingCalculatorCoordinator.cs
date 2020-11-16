using System.Threading.Tasks;
using JournalistTierAPI.Dtos;

namespace JournalistTierAPI.Coordinators
{
    public interface IRatingCalculatorCoordinator
    {
        Task<double> GetRatingsAsync(TierQueryDto tierQueryDto);
    }
}
