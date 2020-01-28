using System.Collections.Generic;

namespace CourseEnrollmentApi.DataAccess.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserCourse> UserCourses { get; set; }
    }
}