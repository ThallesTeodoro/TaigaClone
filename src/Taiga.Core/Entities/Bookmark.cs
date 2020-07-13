using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taiga.Core.Entities
{
    [Table("bookmarks")]
    public class Bookmark
    {
        public int Id { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        // relationships
        public Project Project { get; set; }
        public ICollection<BookmarkUpdate> BookmarkUpdates { get; set; }
    }
}
