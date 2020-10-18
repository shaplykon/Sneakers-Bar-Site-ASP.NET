using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace Sneaker_Bar.Models
{
    [Table("comment")]
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public DateTime Date { get; set; }
    }
}
