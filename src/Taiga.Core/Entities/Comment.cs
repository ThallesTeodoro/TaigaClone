using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taiga.Core.Entities
{
    [Table("comments")]
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public int OwnerId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Text { get; set; }

        [Required]
        public CommentType Type { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }

    public enum CommentType
    {
        Card = 1,
        Issue = 2,
        Bookmark = 3
    }
}
