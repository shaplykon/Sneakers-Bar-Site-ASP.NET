using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sneaker_Bar.Models
{
    [Table("purchase")]
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public int UserId { get; set; }
        public int SneakersId { get; set; }
        public DateTime Date { get; set; }
     //   public bool isConfirmed { get; set; }
    }

    public class PurchaseContext : DbContext { 
    
        public DbSet<Purchase> Purchases { get; set; }
        public PurchaseContext(DbContextOptions<PurchaseContext> options)
           : base(options)
        {
           // Database.EnsureCreated();
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
