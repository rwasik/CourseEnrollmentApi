using System.ComponentModel.DataAnnotations;

namespace CourseEnrollmentApi.Models
{
    public class UserModel
    {
        public string Url { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}