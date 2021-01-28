using System.Collections.Generic;
using System.Threading.Tasks;
using JournalistTierAPI.Model;

namespace JournalistTierAPI.Data
{
    public interface IMediaRepo
    {
        Task<Media> GetMediaByIdAsync(int id);

        Task<bool> AddMediaAsync(Media media);

        Task<IEnumerable<Media>> GetAllMediaAsync();

        Task<bool> RateMediaAsync(UserMediaRating userMediaRating);

        Task<double> GetMediaRatingAsync(UserMediaRating userMediaRating);

        Task<int> GetTotalVotesAsync(int mediaId);
    }
}