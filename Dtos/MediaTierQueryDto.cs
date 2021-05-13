using System.ComponentModel.DataAnnotations;

namespace JournalistTierAPI.Dtos
{
    public class MediaTierQueryDto
    {
        [Required]
        public int MediaId { get; set; }

        public int? TopicId { get; set; }
    }
}