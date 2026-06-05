using _2387701131_NguyenPhiPhuc_KTGK.Data;
using _2387701131_NguyenPhiPhuc_KTGK.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace _2387701131_NguyenPhiPhuc_KTGK.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int page = 1, string? search = null)
        {
            int pageSize = 5;
            var query = _context.Courses.Include(c => c.Category).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Name.Contains(search));
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var courses = await query
                .OrderBy(c => c.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var enrolledCourseIds = new List<int>();
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    enrolledCourseIds = await _context.Enrollments
                        .Where(e => e.UserId == user.Id)
                        .Select(e => e.CourseId)
                        .ToListAsync();
                }
            }

            var viewModel = new CourseListViewModel
            {
                Courses = courses,
                CurrentPage = page,
                TotalPages = totalPages,
                SearchQuery = search,
                EnrolledCourseIds = enrolledCourseIds
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
