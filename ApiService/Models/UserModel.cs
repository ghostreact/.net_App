using System.ComponentModel.DataAnnotations;

namespace ApiService.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; }
    }
}
