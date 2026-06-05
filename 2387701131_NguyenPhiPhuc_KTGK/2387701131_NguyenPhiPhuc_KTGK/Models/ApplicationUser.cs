using Microsoft.AspNetCore.Identity;

namespace _2387701131_NguyenPhiPhuc_KTGK.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
