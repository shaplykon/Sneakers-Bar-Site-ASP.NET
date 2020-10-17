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
        public string Model { get; set; }
        public string Company { get; set; }
        public DateTime ReleaseDate { get; set; }
        public byte[] ImageData { get; set; }
    }
}
