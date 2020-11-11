using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sneaker_Bar.Models
{
    [Table("comment")]
    public class Comment
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Input title")]
        public string Title { get; set; }
        public string Text { get; set; }
        public Guid UserId { get; set; }
        public string AuthorName { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public DateTime Date { get; set; }
    }
}
