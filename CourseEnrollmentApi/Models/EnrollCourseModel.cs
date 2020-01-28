using System.ComponentModel.DataAnnotations;

namespace CourseEnrollmentApi.Models
{
    public class EnrollCourseModel
    {
        [Required]
        public int CourseId { get; set; }
    }
}