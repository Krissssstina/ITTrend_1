using System.ComponentModel.DataAnnotations;

namespace ITTrend.Models
{
    public class User : EntityBase
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<Task> Tasks { get; set; }

    }
}
