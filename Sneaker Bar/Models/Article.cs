using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sneaker_Bar.Models
{
    [Table("article")]
    public class Article
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public int userId { get; set; }
        public DateTime date { get; set; }
    }

    public class ArticleContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public ArticleContext(DbContextOptions<ArticleContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
