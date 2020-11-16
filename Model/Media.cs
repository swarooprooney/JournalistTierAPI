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


        public ICollection<UserMediaRating> UserMediaRatings { get; set; }
    }
}