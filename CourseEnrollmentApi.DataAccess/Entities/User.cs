using System;
using System.Collections.Generic;

namespace CourseEnrollmentApi.DataAccess.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        public virtual ICollection<UserCourse> UserCourses { get; set; }
    }
}