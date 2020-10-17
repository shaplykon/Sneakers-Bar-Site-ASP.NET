using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sneaker_Bar.Models
{
    [Table("article")]
    public class Article
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public int userId { get; set; }
        public User user { get; set; }
        public DateTime date { get; set; }
    }    
}
