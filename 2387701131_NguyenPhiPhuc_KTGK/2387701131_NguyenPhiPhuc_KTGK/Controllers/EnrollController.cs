using _2387701131_NguyenPhiPhuc_KTGK.Data;
using _2387701131_NguyenPhiPhuc_KTGK.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _2387701131_NguyenPhiPhuc_KTGK.Controllers
{
    [Authorize(Roles = "STUDENT")]
    [Route("enroll")]
    public class EnrollController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EnrollController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // POST: /enroll/register/5
        [HttpPost]
        [Route("register/{courseId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(int courseId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var alreadyEnrolled = await _context.Enrollments
                .AnyAsync(e => e.UserId == user.Id && e.CourseId == courseId);

            if (alreadyEnrolled)
            {
                TempData["Error"] = "Bạn đã đăng ký học phần này rồi!";
                return RedirectToAction("Index", "Home");
            }

            var enrollment = new Enrollment
            {
                UserId = user.Id,
                CourseId = courseId,
                EnrollDate = DateTime.Now
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Đăng ký học phần thành công!";
            return RedirectToAction("Index", "Home");
        }

        // POST: /enroll/unenroll/5
        [HttpPost]
        [Route("unenroll/{courseId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Unenroll(int courseId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.UserId == user.Id && e.CourseId == courseId);

            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Hủy đăng ký học phần thành công!";
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: /enroll/mycourses
        [Route("mycourses")]
        public async Task<IActionResult> MyCourses()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var enrollments = await _context.Enrollments
                .Include(e => e.Course)
                .ThenInclude(c => c!.Category)
                .Where(e => e.UserId == user.Id)
                .OrderByDescending(e => e.EnrollDate)
                .ToListAsync();

            return View(enrollments);
        }
    }
}
