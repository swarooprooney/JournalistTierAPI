using System.ComponentModel.DataAnnotations;

namespace JournalistTierAPI.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}