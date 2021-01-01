using System;
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

        [Required]
        [MaxLength]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<UserJournalistRating> UserJournalistRatings { get; set; }
    }
}