using System.ComponentModel.DataAnnotations;

namespace TennisCoach.Models
{
    public class Members
    {
        [Key]
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool? Active { get; set; }

        public string Password { get; set; }
    }
}
