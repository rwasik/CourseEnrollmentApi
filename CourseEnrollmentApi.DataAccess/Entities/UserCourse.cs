using System;

namespace CourseEnrollmentApi.DataAccess.Entities
{
    public class UserCourse
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}