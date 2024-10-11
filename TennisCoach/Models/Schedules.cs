using System;
using System.ComponentModel.DataAnnotations;

namespace TennisCoach.Models
{
    public class Schedules
    {
        [Key]
        public int ScheduleId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

       
    }
}
