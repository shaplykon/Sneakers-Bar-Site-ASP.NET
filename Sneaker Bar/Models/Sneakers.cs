using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Sneaker_Bar.Models
{
    [Table("sneakers")]
    public class Sneakers
    {
        public int Id { get; set; }
        public double Price { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public byte[] ImageData { get; set; }
    }

    public class SneakersContext : DbContext {
        public SneakersContext(DbContextOptions<SneakersContext> options)
            : base(options)
        {  }
        public DbSet<Sneakers> Sneakers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
