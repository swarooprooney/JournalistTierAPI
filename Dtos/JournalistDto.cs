using System;

namespace JournalistTierAPI.Dtos
{
    public class JournalistDto
    {
        public int JournalistId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string PhotoUrl { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}