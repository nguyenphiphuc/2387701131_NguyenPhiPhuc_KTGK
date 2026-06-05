namespace _2387701131_NguyenPhiPhuc_KTGK.Models
{
    public class CourseListViewModel
    {
        public IEnumerable<Course> Courses { get; set; } = new List<Course>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? SearchQuery { get; set; }
        public List<int> EnrolledCourseIds { get; set; } = new List<int>();
    }

    public class DashboardViewModel
    {
        public int TotalCourses { get; set; }
        public int TotalStudents { get; set; }
        public int TotalEnrollments { get; set; }
        public List<CategoryStat> CategoryStats { get; set; } = new List<CategoryStat>();
        public List<CourseStat> TopCourses { get; set; } = new List<CourseStat>();
    }

    public class CategoryStat
    {
        public string CategoryName { get; set; } = string.Empty;
        public int CourseCount { get; set; }
    }

    public class CourseStat
    {
        public string CourseName { get; set; } = string.Empty;
        public int EnrollmentCount { get; set; }
    }
}
