using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taiga.Core.Entities
{
    [Table("attachments")]
    public class Attachment
    {
        public int Id { get; set; }

        [Required]
        public int OwnerId { get; set; }

        [Required]
        public string File { get; set; }

        [Required]
        public AttachmentType Type { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }

    public enum AttachmentType
    {
        Card = 1,
        Issue = 2,
        Bookmark = 3
    }
}
