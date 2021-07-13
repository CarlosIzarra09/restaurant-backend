using System.ComponentModel.DataAnnotations;

namespace DSonia.API.Domain.Services
{
    public class AuthenticationRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}