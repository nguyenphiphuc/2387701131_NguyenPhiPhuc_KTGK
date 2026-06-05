using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2387701131_NguyenPhiPhuc_KTGK.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên học phần là bắt buộc")]
        [Display(Name = "Tên học phần")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Hình ảnh")]
        public string? Image { get; set; }

        [Required(ErrorMessage = "Số tín chỉ là bắt buộc")]
        [Range(1, 10, ErrorMessage = "Số tín chỉ từ 1 đến 10")]
        [Display(Name = "Số tín chỉ")]
        public int Credits { get; set; }

        [Required(ErrorMessage = "Giảng viên là bắt buộc")]
        [Display(Name = "Giảng viên")]
        public string Lecturer { get; set; } = string.Empty;

        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
