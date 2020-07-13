using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taiga.Core.Entities
{
    [Table("attempts_quantities")]
    public class AttemptsQuantity
    {
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public EndpointType Type { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }

    public enum EndpointType
    {
        ConfirmEmail = 1,
        TowFactor = 2,
        ForgotPassword = 3
    }
}
