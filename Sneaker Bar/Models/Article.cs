using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sneaker_Bar.Models
{
    [Table("article")]
    public class Article
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Input Tile")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Input article body")]
        public string Text { get; set; }
        [Required(ErrorMessage = "Select Image")]
        public string ImageData { get; set; }
        public Guid UserId { get; set; }
        public string AuthorName { get; set; }
        public DateTime Date { get; set; }
 
        
    }
}
