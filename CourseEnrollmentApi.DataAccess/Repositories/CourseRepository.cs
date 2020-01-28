using System;
using System.Linq;
using CourseEnrollmentApi.DataAccess.Context;
using CourseEnrollmentApi.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseEnrollmentApi.DataAccess.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _dataContext;

        public CourseRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IQueryable<Course> GetCourses()
        {
            return _dataContext.Courses;
        }

        public IQueryable<Course> GetCoursesWithUsers()
        {
            return _dataContext.Courses
                .Include(c => c.UserCourses);
        }

        public IQueryable<Course> GetCoursesForUser(Guid userId)
        {
            return GetCoursesWithUsers()
                .Where(c => c.UserCourses.Any(c => c.UserId == userId));
        }

        public Course GetCourse(int id)
        {
            return GetCourses().SingleOrDefault(c => c.Id == id);
        }

        public Course GetCourseWithUsers(int id)
        {
            return _dataContext.Courses
                .Include(c => c.UserCourses)
                .SingleOrDefault(c => c.Id == id);
        }

        public void AddCourse(Course course)
        {
            _dataContext.Courses.Add(course);
        }

        public void DeleteCourse(Course course)
        {
            _dataContext.Remove(course);
        }                

        public bool Save()
        {
            return _dataContext.SaveChanges() > 0;
        }
    }
}
