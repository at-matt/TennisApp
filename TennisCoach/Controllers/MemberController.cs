using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TennisCoach.Data;
using TennisCoach.Models; // Add this line to include your Member model
 // Add this for your view model
using System.Threading.Tasks;

namespace TennisCoach.Controllers
{
    public class MemberController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MemberController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Coach()
        {
            return View();
        }

        public async Task<IActionResult> Coaches()
        {
            // Retrieve the list of coaches
            var coaches = await _context.Coaches.ToListAsync();
            // Log the count of retrieved coaches for debugging
            Console.WriteLine($"Coaches retrieved: {coaches.Count}");
            // Pass the list of coaches to the view
            return View(coaches);
        }
       
    }
}

        
  
