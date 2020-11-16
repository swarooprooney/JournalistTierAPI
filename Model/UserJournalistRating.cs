using System.ComponentModel.DataAnnotations;

namespace JournalistTierAPI.Model
{
    public class UserJournalistRating
    {
        [Key]
        public int UserJournalistRatingId { get; set; }

        [Required]
        public int JournalistId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int TopicId { get; set; }

        [Required]
        public int Rating { get; set; }

        public Journalist Journalist { get; set; }

        public User User { get; set; }

        public Topic Topic { get; set; }
    }
}