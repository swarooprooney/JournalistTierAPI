using System.Collections.Generic;
using System.Threading.Tasks;
using JournalistTierAPI.Model;

namespace JournalistTierAPI.Data
{
    public interface IJournalistRepo
    {
        Task<Journalist> GetJournalistByIdAsync(int id);

        Task<bool> AddJournalistAsync(Journalist journalist);

        Task<IEnumerable<Journalist>> GetAllJournalistAsync();

        Task<bool> RateJournalistAsync(UserJournalistRating userJournalistRating);

        Task<double> GetJournalistRatingAsync(UserJournalistRating userJournalistRating);
    }
}