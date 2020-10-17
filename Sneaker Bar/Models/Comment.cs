using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace Sneaker_Bar.Models
{
    [Table("comment")]
    public class Comment
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public int userId { get; set; }
        public int articleId { get; set; }
        public Article article { get; set; }
        public DateTime date { get; set; }
    }
}
