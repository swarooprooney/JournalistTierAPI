using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JournalistTierAPI.Model
{
    public class Topic
    {
        [Key]
        public int TopicId { get; set; }

        [Required]
        public string Name { get; set; }
        public ICollection<UserJournalistRating> UserJournalistRatings { get; set; }

        public ICollection<UserMediaRating> UserMediaRatings { get; set; }
    }
}