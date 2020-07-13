using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taiga.Core.Entities
{
    [Table("cards")]
    public class Card
    {
        public int Id { get; set; }

        [Required]
        public int KanbanColumnId { get; set; }

        [Required]
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAT { get; set; }

        // relationships
        public KanbanColumn KanbanColumn { get; set; }
        public User Owner { get; set; }
        public ICollection<CardPoint> CardPoints { get; set; }
    }
}
