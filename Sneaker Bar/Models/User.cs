using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace Sneaker_Bar.Models
{
    [Table("users")]
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
    }
    [Table("roles")]
    public class Role {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}