using System.ComponentModel.DataAnnotations;

namespace JournalistTierAPI.Dtos
{
    public class CreateJournalistDto
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string PhotoUrl { get; set; }
    }
}