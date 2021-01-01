using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JournalistTierAPI.Dtos;
using JournalistTierAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace JournalistTierAPI.Data
{
    public class JournalistRepo : IJournalistRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public JournalistRepo(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

        public async Task<bool> AddJournalistAsync(Journalist journalist)
        {
            await _context.Journalist.AddAsync(journalist);
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<IEnumerable<JournalistDto>> GetAllJournalistAsync()
        {
            return await _context.Journalist.Where(x => x.IsApproved == true).ProjectTo<JournalistDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<JournalistDto> GetJournalistByIdAsync(int id)
        {
            return await _context.Journalist.ProjectTo<JournalistDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.JournalistId == id);
        }

        public Task<double> GetJournalistRatingAsync(UserJournalistRating userJournalistRating)
        {
            if (_context.UserJournalistRating.Any(x => x.JournalistId == userJournalistRating.JournalistId))
            {
                if (userJournalistRating.TopicId <= 0)
                {
                    return _context.UserJournalistRating.Where(m => m.JournalistId == userJournalistRating.JournalistId).AverageAsync(x => x.Rating);
                }
                return _context.UserJournalistRating.Where(m => m.JournalistId == userJournalistRating.JournalistId && m.TopicId == userJournalistRating.TopicId).AverageAsync(x => x.Rating);
            }
            else
            {
                return Task.FromResult(0.0d);
            }
        }

        public async Task<bool> RateJournalistAsync(UserJournalistRating userJournalistRating)
        {

            var userJournalistRatingLocal = await _context.UserJournalistRating.AsNoTracking().FirstOrDefaultAsync(x => x.JournalistId == userJournalistRating.JournalistId
            && x.TopicId == userJournalistRating.TopicId && x.UserId == userJournalistRating.UserId);
            if (userJournalistRatingLocal != null)
            {
                userJournalistRating.UserJournalistRatingId = userJournalistRatingLocal.UserJournalistRatingId;
                _context.UserJournalistRating.Update(userJournalistRating);
            }
            else
            {
                await _context.UserJournalistRating.AddAsync(userJournalistRating);
            }
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
    }
}