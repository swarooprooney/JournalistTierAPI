using System.ComponentModel.DataAnnotations;

namespace JournalistTierAPI.Dtos
{
    public class CreateTopicDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

    }
}