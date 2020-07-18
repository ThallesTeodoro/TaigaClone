using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Taiga.Api.Features.Register.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public IFormFile Avatar { get; set; }

        [DataType(DataType.Text)]
        public string Biography { get; set; }
    }
}
