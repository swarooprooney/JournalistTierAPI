using System.ComponentModel.DataAnnotations;

namespace JournalistTierAPI.Dtos
{
    public class MediaDto
    {
        public int MediaId { get; set; }

        [Required]
        public string Name { get; set; }

        public string PhotoUrl { get; set; }
        [Required]
        public string Description { get; set; }
    }
}