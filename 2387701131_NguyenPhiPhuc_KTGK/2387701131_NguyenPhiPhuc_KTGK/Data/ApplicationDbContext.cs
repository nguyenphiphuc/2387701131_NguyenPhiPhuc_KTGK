using _2387701131_NguyenPhiPhuc_KTGK.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _2387701131_NguyenPhiPhuc_KTGK.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed Categories
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Công nghệ thông tin" },
                new Category { Id = 2, Name = "Kinh tế" },
                new Category { Id = 3, Name = "Ngoại ngữ" },
                new Category { Id = 4, Name = "Khoa học cơ bản" }
            );

            // Seed Courses
            builder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "Lập trình Web", Credits = 3, Lecturer = "TS. Nguyễn Văn A", CategoryId = 1, Image = "/images/courses/web.jpg" },
                new Course { Id = 2, Name = "Cơ sở dữ liệu", Credits = 3, Lecturer = "PGS.TS. Trần Thị B", CategoryId = 1, Image = "/images/courses/database.jpg" },
                new Course { Id = 3, Name = "Trí tuệ nhân tạo", Credits = 4, Lecturer = "TS. Lê Văn C", CategoryId = 1, Image = "/images/courses/ai.jpg" },
                new Course { Id = 4, Name = "Kinh tế vi mô", Credits = 3, Lecturer = "TS. Phạm Thị D", CategoryId = 2, Image = "/images/courses/economics.jpg" },
                new Course { Id = 5, Name = "Marketing căn bản", Credits = 2, Lecturer = "ThS. Hoàng Văn E", CategoryId = 2, Image = "/images/courses/marketing.jpg" },
                new Course { Id = 6, Name = "Tiếng Anh giao tiếp", Credits = 2, Lecturer = "ThS. Ngô Thị F", CategoryId = 3, Image = "/images/courses/english.jpg" },
                new Course { Id = 7, Name = "Mạng máy tính", Credits = 3, Lecturer = "TS. Đặng Văn G", CategoryId = 1, Image = "/images/courses/network.jpg" },
                new Course { Id = 8, Name = "Toán rời rạc", Credits = 3, Lecturer = "PGS.TS. Vũ Thị H", CategoryId = 4, Image = "/images/courses/math.jpg" },
                new Course { Id = 9, Name = "Lập trình di động", Credits = 3, Lecturer = "TS. Bùi Văn I", CategoryId = 1, Image = "/images/courses/mobile.jpg" },
                new Course { Id = 10, Name = "An toàn thông tin", Credits = 3, Lecturer = "TS. Đinh Văn K", CategoryId = 1, Image = "/images/courses/security.jpg" },
                new Course { Id = 11, Name = "Quản trị kinh doanh", Credits = 3, Lecturer = "TS. Lý Thị L", CategoryId = 2, Image = "/images/courses/business.jpg" },
                new Course { Id = 12, Name = "Xác suất thống kê", Credits = 3, Lecturer = "PGS.TS. Mai Văn M", CategoryId = 4, Image = "/images/courses/statistics.jpg" }
            );
        }
    }
}
