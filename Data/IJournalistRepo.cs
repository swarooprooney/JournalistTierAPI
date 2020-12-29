using System.Collections.Generic;
using System.Threading.Tasks;
using JournalistTierAPI.Dtos;
using JournalistTierAPI.Model;

namespace JournalistTierAPI.Data
{
    public interface IJournalistRepo
    {
        Task<JournalistDto> GetJournalistByIdAsync(int id);

        Task<bool> AddJournalistAsync(Journalist journalist);

        Task<IEnumerable<JournalistDto>> GetAllJournalistAsync();

        Task<bool> RateJournalistAsync(UserJournalistRating userJournalistRating);

        Task<double> GetJournalistRatingAsync(UserJournalistRating userJournalistRating);
    }
}