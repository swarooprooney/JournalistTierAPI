using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JournalistTierAPI.Model
{
    public class Journalist
    {
        [Key]
        public int JournalistId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<UserJournalistRating> UserJournalistRatings { get; set; }
    }
}