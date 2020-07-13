using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Taiga.Core.Entities
{
    [Table("users")]
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [JsonIgnore]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Avatar { get; set; }

        [DataType(DataType.Text)]
        public string Biography { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAT { get; set; }

        [DefaultValue(false)]
        public bool EmailConfirmed { get; set; }

        // relationships
        [ForeignKey("OwnerId")]
        public ICollection<Project> Projects { get; set; }
        public ICollection<Team> Teams { get; set; }
        public ICollection<BookmarkUpdate> BookmarkUpdates { get; set; }
        public ICollection<Card> Cards { get; set; }
    }
}
