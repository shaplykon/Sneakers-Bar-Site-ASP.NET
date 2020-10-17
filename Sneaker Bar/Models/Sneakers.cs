using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Sneaker_Bar.Models
{
    [Table("sneakers")]
    public class Sneakers
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please input price")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Please input model")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Please input company")]
        public string Company { get; set; }

        [Required(ErrorMessage = "Please choose date")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Please choose image")]
        [Display(Name = "Sneakers Picture")]
        public string ImageData { get; set; }
    }
}
