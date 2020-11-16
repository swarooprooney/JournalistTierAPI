using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JournalistTierAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace JournalistTierAPI.Data
{
    public class TopicRepo : ITopicRepo
    {
        private readonly DataContext _context;
        public TopicRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<Topic> GetTopicFromDbByIdAsync(int id)
        {
            return await _context.Topic.FirstOrDefaultAsync(x => x.TopicId == id);
        }

        public async Task<IEnumerable<Topic>> GetTopicsAsync()
        {
            return await _context.Topic.ToListAsync();
        }

        public async Task<bool> InsertTopicToDatabaseAsync(Topic topic)
        {
            await _context.Topic.AddAsync(topic);
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
    }
}