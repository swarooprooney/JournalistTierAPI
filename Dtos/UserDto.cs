using System;

namespace JournalistTierAPI.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Token { get; set; }

        public string KnownAs { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string PhotoUrl { get; set; }
    }
}