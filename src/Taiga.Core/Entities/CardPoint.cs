using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taiga.Core.Entities
{
    [Table("card_points")]
    public class CardPoint
    {
        public int Id { get; set; }

        [Required]
        public int CardId { get; set; }

        [Required]
        public double Value { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAT { get; set; }

        // relationships
        public Card Card { get; set; }
    }
}
