using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolStudentAPI.Entities
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
