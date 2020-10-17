using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sneaker_Bar.Models
{
    [Table("comment")]
    public class Comment
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public int userId { get; set; }
        public int articleId { get; set; }
        public DateTime date { get; set; }
    }

    public class CommentContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public CommentContext(DbContextOptions<CommentContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
