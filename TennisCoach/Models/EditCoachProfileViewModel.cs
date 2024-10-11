namespace TennisCoach.Models
{
    public class EditCoachProfileViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public IFormFile PhotoFile { get; set; } // This is used for file uploads
    }

}
