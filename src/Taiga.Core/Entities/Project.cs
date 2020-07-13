using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taiga.Core.Entities
{
    [Table("projects")]
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Slug { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        public string Image { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAT { get; set; }

        // relationships
        public User Owner { get; set; }
        public ICollection<Bookmark> Bookmarks { get; set; }
    }
}
