
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sneaker_Bar.Models
{
    [Table("purchase")]
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public int UserId { get; set; }
    //    public User User { get; set; }
        public int SneakersId { get; set; }
        public Sneakers Sneakers { get; set; }
        public DateTime Date { get; set; }
        //   public bool isConfirmed { get; set; }
    }
}
