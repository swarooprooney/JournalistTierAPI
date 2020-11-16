using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JournalistTierAPI.Model
{
    public class UserMediaRating
    {
        [Key]
        public int UserMediaRatingId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int TopicId { get; set; }

        [Required]
        public int MediaId { get; set; }

        [Required]
        public int Rating { get; set; }

        public Media Media { get; set; }

        public User User { get; set; }

        public Topic Topic { get; set; }
    }
}