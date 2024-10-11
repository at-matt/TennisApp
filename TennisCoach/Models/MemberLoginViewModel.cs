using System.ComponentModel.DataAnnotations;

namespace TennisCoach.Models
{
    public class MemberLoginViewModel
    {
      
   
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

}
