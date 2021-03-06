using System.ComponentModel.DataAnnotations;

namespace JournalistTierAPI.Dtos
{
    public class JournalistTierQueryDto
    {
        [Required]
        public int JournalistId { get; set; }

        public int? MediaId { get; set; }

        public int? TopicId { get; set; }
    }
}