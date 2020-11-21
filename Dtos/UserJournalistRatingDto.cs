using System.ComponentModel.DataAnnotations;

namespace JournalistTierAPI.Dtos
{
    public class UserJournalistRatingDto
    {
        [Required(ErrorMessage = "User Id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "User Id is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Journalist Id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Journalist Id is required")]
        public int JournalistId { get; set; }

        [Required(ErrorMessage = "Topic Id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Topic Id is required")]
        public int TopicId { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        [Range(1, 5, ErrorMessage = "Rating should be between 1 to 5")]
        public int Rating { get; set; }
    }

}