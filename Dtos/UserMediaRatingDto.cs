using System.ComponentModel.DataAnnotations;

namespace JournalistTierAPI.Dtos
{
    public class UserMediaRatingDto
    {
        [Required(ErrorMessage = "User Id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "User Id is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Media Id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Media Id is required")]
        public int MediaId { get; set; }

        [Required(ErrorMessage = "Topic Id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Topic Id is required")]
        public int TopicId { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        [Range(1, 5, ErrorMessage = "Rating should be between 1 to 5")]
        public int Rating { get; set; }
    }

}