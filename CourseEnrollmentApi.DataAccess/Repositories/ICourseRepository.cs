using System;
using System.Linq;
using CourseEnrollmentApi.DataAccess.Entities;

namespace CourseEnrollmentApi.DataAccess.Repositories
{
    public interface ICourseRepository
    {
        IQueryable<Course> GetCourses();
        IQueryable<Course> GetCoursesWithUsers();
        IQueryable<Course> GetCoursesForUser(Guid userId);
        Course GetCourse(int id);
        Course GetCourseWithUsers(int id);
        void AddCourse(Course user);
        void DeleteCourse(Course course);
        bool Save();
    }
}
