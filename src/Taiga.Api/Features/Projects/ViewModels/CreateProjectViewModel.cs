using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Taiga.Api.Features.Projects.ViewModels
{
    public class CreateProjectViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public IFormFile Image { get; set; }
    }
}
