using System.ComponentModel.DataAnnotations;

namespace TennisCoach.Models
{
        public class CoachLoginViewModel
        {


        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }



    }
}
