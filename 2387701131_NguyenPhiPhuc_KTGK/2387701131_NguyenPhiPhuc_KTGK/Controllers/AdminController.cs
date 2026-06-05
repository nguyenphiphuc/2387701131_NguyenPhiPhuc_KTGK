using _2387701131_NguyenPhiPhuc_KTGK.Data;
using _2387701131_NguyenPhiPhuc_KTGK.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace _2387701131_NguyenPhiPhuc_KTGK.Controllers
{
    [Authorize(Roles = "ADMIN")]
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _environment;

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
        }

        // GET: /admin
        [Route("")]
        [Route("dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            var totalCourses = await _context.Courses.CountAsync();
            var totalEnrollments = await _context.Enrollments.CountAsync();

            var studentRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "STUDENT");
            var totalStudents = 0;
            if (studentRole != null)
            {
                totalStudents = await _context.UserRoles.CountAsync(ur => ur.RoleId == studentRole.Id);
            }

            var categoryStats = await _context.Categories
                .Select(c => new CategoryStat
                {
                    CategoryName = c.Name,
                    CourseCount = c.Courses.Count
                })
                .ToListAsync();

            var topCourses = await _context.Courses
                .Select(c => new CourseStat
                {
                    CourseName = c.Name,
                    EnrollmentCount = c.Enrollments.Count
                })
                .OrderByDescending(c => c.EnrollmentCount)
                .Take(5)
                .ToListAsync();

            var viewModel = new DashboardViewModel
            {
                TotalCourses = totalCourses,
                TotalStudents = totalStudents,
                TotalEnrollments = totalEnrollments,
                CategoryStats = categoryStats,
                TopCourses = topCourses
            };

            return View(viewModel);
        }

        // GET: /admin/courses
        [Route("courses")]
        public async Task<IActionResult> Courses()
        {
            var courses = await _context.Courses.Include(c => c.Category).ToListAsync();
            return View(courses);
        }

        // GET: /admin/create
        [Route("create")]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: /admin/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> Create(Course course, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "courses");
                    Directory.CreateDirectory(uploadsFolder);
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                    course.Image = "/images/courses/" + uniqueFileName;
                }

                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Tạo học phần thành công!";
                return RedirectToAction("Courses");
            }
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", course.CategoryId);
            return View(course);
        }

        // GET: /admin/edit/5
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", course.CategoryId);
            return View(course);
        }

        // POST: /admin/edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id, Course course, IFormFile? imageFile)
        {
            if (id != course.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingCourse = await _context.Courses.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
                    if (existingCourse == null) return NotFound();

                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "courses");
                        Directory.CreateDirectory(uploadsFolder);
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }
                        course.Image = "/images/courses/" + uniqueFileName;
                    }
                    else
                    {
                        course.Image = existingCourse.Image;
                    }

                    _context.Update(course);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật học phần thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Courses.Any(e => e.Id == id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction("Courses");
            }
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", course.CategoryId);
            return View(course);
        }

        // GET: /admin/delete/5
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses.Include(c => c.Category).FirstOrDefaultAsync(c => c.Id == id);
            if (course == null) return NotFound();
            return View(course);
        }

        // POST: /admin/delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Xóa học phần thành công!";
            }
            return RedirectToAction("Courses");
        }
    }
}
