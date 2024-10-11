using Microsoft.AspNetCore.Mvc;
using TennisCoach.Data;
using TennisCoach.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace TennisCoach.Controllers
{
    public class CoachController : Controller
    {
        private readonly ApplicationDbContext _context;

        


        public CoachController(ApplicationDbContext context)
        {
            _context = context;
        }
        private int GetCurrentCoachId()
        {
            var CoachIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get CoachId from logged-in user
            if (string.IsNullOrEmpty(CoachIdClaim))
            {
                return 0; // or throw an exception, or handle it as per your application's needs
            }

            // Parse the CoachIdClaim to an integer and return it
            if (int.TryParse(CoachIdClaim, out int CoachId))
            {
                return CoachId;
            }

            return 0; // If parsing fails, return 0 or handle accordingly
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Details()
        {
            var CoachId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(CoachId))
            {
                return Unauthorized();
            }

            var Coach = _context.Coaches.SingleOrDefault(c => c.CoachId == int.Parse(CoachId));

            if (Coach == null)
            {
                return NotFound();
            }

            var CoachProfileViewModel = new CoachProfileViewModel
            {
                FirstName = Coach.FirstName.Trim(),
                LastName = Coach.LastName.Trim(),
                Biography = Coach.Biography,
                Photo = Coach.Photo
            };

            return View(Coach);
        }

        public IActionResult Create(int id)
        {
            var Coach = new Coaches { CoachId = id };
            return View(Coach);
        }
        public IActionResult CreateProfile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfile(Coaches Coach)
        {
            int userId = GetCurrentCoachId();

            if (userId == 0)
            {
                return NotFound();
            }

            Coach.CoachId = userId;

            _context.Coaches.Update(Coach);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = userId });
        }
    

    // GET: Coach/EditProfile
    public async Task<IActionResult> EditProfile()
        {
            int userId = GetCurrentCoachId();
            return RedirectToAction("Edit", new { id = userId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var Coach = await _context.Coaches.FindAsync(id);
            if (Coach == null)
            {
                return NotFound();
            }

            var model = new EditCoachProfileViewModel
            {
                FirstName = Coach.FirstName,
                LastName = Coach.LastName,
                Biography = Coach.Biography,
                // Populate other fields as needed
            };

            return View(model); // This should render Views/Coach/Edit.cshtml
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCoachProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Return the view with the model to show validation errors
            }

            int userId = GetCurrentCoachId();
            var existingCoach = await _context.Coaches.FindAsync(userId);

            if (existingCoach == null)
            {
                return NotFound();
            }

            existingCoach.FirstName = model.FirstName;
            existingCoach.LastName = model.LastName;
            existingCoach.Biography = model.Biography;

            if (model.PhotoFile != null && model.PhotoFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await model.PhotoFile.CopyToAsync(memoryStream);
                    existingCoach.Photo = memoryStream.ToArray(); // Assuming Photo is byte[]
                }
            }

            _context.Coaches.Update(existingCoach);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details"); // Redirect to the details page or another appropriate action
        }




        public async Task<IActionResult> UpcomingSchedules()
        {
            var schedules = await _context.Schedules.ToListAsync();
            return View(schedules);
        }

        public async Task<IActionResult> EnrolledMembers()
        {
            var members
                = await _context.Members.ToListAsync();
            return View(members);
        }
    }
}

