using System.ComponentModel.DataAnnotations;

namespace Taiga.Api.Features.Register.ViewModels
{
    public class ConfirmCodeViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public int Code { get; set; }
    }
}
