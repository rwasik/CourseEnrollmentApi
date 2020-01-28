using System;
using System.Collections.Generic;
using CourseEnrollmentApi.DataAccess.Entities;
using CourseEnrollmentApi.Shared.Enums;

namespace CourseEnrollmentApi.Services.Services
{
    public interface ICourseService
    {
        IEnumerable<Course> GetCourses();
        IEnumerable<Course> GetCoursesWithUsers();
        IEnumerable<Course> GetCoursesForUser(Guid userId);
        Course GetCourse(int id);
        void AddCourse(Course course);
        ActionStatus AddCourseForUser(Guid userId, int id);
        ActionStatus DeleteCourse(int id);
        ActionStatus DeleteCourseForUser(Guid userId, int id);        
    }
}
