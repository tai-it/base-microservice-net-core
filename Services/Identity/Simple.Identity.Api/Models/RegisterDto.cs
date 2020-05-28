namespace Simple.Identity.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterDto
    {
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email address is invalid format")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 8)]
        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }
    }
}
