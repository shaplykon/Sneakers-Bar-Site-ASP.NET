using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sneaker_Bar.Models
{
    [Table("purchase")]
    public class Purchase
    {
        public int purchaseId { get; set; }
        public int userId { get; set; }
        public User user { get; set; }
        public int sneakersId { get; set; }
        public Sneakers sneakers { get; set; }
        public DateTime Date { get; set; }
        //   public bool isConfirmed { get; set; }
    }
}
