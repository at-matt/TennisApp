namespace TennisCoach.Models
{
    public class Coaches
    {
        
       public int CoachId { get; set; } // Primary Key (will auto-increment)
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Biography { get; set; } // Optional field for coach biography
        public byte[]? Photo { get; set; } // Optional field for storing a photo URL or path
        public string Email { get; set; } // For email login
        public string Password { get; set; } // Password (hashed in production)
        
    }
}
