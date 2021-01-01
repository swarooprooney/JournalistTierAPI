using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JournalistTierAPI.Model
{
    public class Media
    {
        [Key]
        public int MediaId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public bool IsApproved { get; set; } = false;

        public ICollection<UserMediaRating> UserMediaRatings { get; set; }
    }
}