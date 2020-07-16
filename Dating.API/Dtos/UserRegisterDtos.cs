using System.ComponentModel.DataAnnotations;

namespace Dating.API.Dtos
{
    public class UserRegisterDtos
    {
        [Required]
        public string Username {get; set;}
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You need to set upto 8 characters")]
        public string Password {get; set;}
    }
}