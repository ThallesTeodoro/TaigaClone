using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taiga.Core.Entities
{
    [Table("teams")]
    public class Team
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAT { get; set; }

        // relationships
        public Project Project { get; set; }
        public User User { get; set; }
    }
}
