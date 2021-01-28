using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JournalistTierAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JournalistTierAPI.Data
{
    public class MediaRepo : IMediaRepo
    {
        private readonly DataContext _context;

        public MediaRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddMediaAsync(Media media)
        {
            await _context.Media.AddAsync(media);
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<IEnumerable<Media>> GetAllMediaAsync()
        {
            return await _context.Media.ToListAsync();
        }

        public async Task<Media> GetMediaByIdAsync(int id)
        {
            return await _context.Media.FirstOrDefaultAsync(x => x.MediaId == id);
        }

        public Task<double> GetMediaRatingAsync(UserMediaRating userMediaRating)
        {
            if (_context.UserMediaRating.Any(x => x.MediaId == userMediaRating.MediaId))
            {
                if (userMediaRating.TopicId <= 0)
                {
                    return _context.UserMediaRating.Where(m => m.MediaId == userMediaRating.MediaId).AverageAsync(x => x.Rating);
                }
                return _context.UserMediaRating.Where(m => m.MediaId == userMediaRating.MediaId && m.TopicId == userMediaRating.TopicId).AverageAsync(x => x.Rating);
            }
            else
            {
                return Task.FromResult(0.0d);
            }
        }

        public async Task<int> GetTotalVotesAsync(int mediaId)
        {
            return await _context.Media.Where(x => x.MediaId == mediaId).CountAsync();
        }

        public async Task<bool> RateMediaAsync(UserMediaRating userMediaRating)
        {
            var userMediaRatingLocal = await _context.UserMediaRating.AsNoTracking().FirstOrDefaultAsync(x => x.MediaId == userMediaRating.MediaId
           && x.TopicId == userMediaRating.TopicId && x.UserId == userMediaRating.UserId);
            if (userMediaRatingLocal != null)
            {
                userMediaRating.UserMediaRatingId = userMediaRatingLocal.UserMediaRatingId;
                _context.UserMediaRating.Update(userMediaRating);
            }
            else
            {
                await _context.UserMediaRating.AddAsync(userMediaRating);
            }
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
    }
}