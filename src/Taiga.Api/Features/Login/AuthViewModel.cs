using System.ComponentModel.DataAnnotations;

namespace Taiga.Api.Features.Login
{
    public class AuthViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
