using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taiga.Core.Entities
{
    [Table("email_confirmation_codes")]
    public class EmailConfirmationCode
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int Code { get; set; }

        [Required]
        public CodeType Type { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }

    public enum CodeType
    {
        Login = 1,
        Register = 2
    }
}
