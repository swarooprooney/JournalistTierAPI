using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JournalistTierAPI.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        public ICollection<UserJournalistRating> UserJournalistRatings { get; set; }

        public ICollection<UserMediaRating> UserMediaRatings { get; set; }
    }
}