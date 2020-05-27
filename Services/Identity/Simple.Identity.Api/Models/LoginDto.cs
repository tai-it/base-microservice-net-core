namespace Simple.Identity.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
