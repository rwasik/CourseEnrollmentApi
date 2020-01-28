using System.ComponentModel.DataAnnotations;

namespace CourseEnrollmentApi.Models
{
    public class CourseModel
    {
        public string Url { get; set; }

        [Required]
        public string Name { get; set; }        
    }
}
