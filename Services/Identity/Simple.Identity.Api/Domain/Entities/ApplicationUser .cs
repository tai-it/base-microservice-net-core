using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Simple.Identity.Api.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string SecurityNumber { get; set; }

        [RegularExpression(@"(0[1-9]|1[0-2])\/[0-9]{2}", ErrorMessage = "Expiration should match a valid MM/YY value")]
        public string Expiration { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
    }
}
