using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taiga.Core.Entities
{
    [Table("bookmark_updates")]
    public class BookmarkUpdate
    {
        public int Id { get; set; }

        [Required]
        public int BookmarkId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        // relationships
        public Bookmark Bookmark { get; set; }
        public User User { get; set; }
    }
}
