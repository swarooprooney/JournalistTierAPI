using System.Collections.Generic;
using System.Threading.Tasks;
using JournalistTierAPI.Model;

namespace JournalistTierAPI.Data
{
    public interface ITopicRepo
    {
        Task<bool> InsertTopicToDatabaseAsync(Topic topic);

        Task<Topic> GetTopicFromDbByIdAsync(int id);

        Task<IEnumerable<Topic>> GetTopicsAsync();
    }
}