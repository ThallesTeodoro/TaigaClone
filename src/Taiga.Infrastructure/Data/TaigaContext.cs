using Microsoft.EntityFrameworkCore;
using Taiga.Core.Entities;

namespace Taiga.Infrastructure.Data
{
    public class TaigaContext : DbContext
    {
        public TaigaContext(DbContextOptions<TaigaContext> options) : base(options) {}

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<AttemptsQuantity> AttemptsQuantitys { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<BookmarkUpdate> BookmarkUpdates { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardPoint> CardPoints { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<EmailConfirmationCode> EmailConfirmationCodes { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<KanbanColumn> KanbanColumns { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
