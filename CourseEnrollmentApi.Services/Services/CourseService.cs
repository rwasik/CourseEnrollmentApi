using System;
using System.Collections.Generic;
using System.Linq;
using CourseEnrollmentApi.DataAccess.Entities;
using CourseEnrollmentApi.DataAccess.Repositories;
using CourseEnrollmentApi.Shared.Enums;

namespace CourseEnrollmentApi.Services.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public IEnumerable<Course> GetCourses()
        {
            return _courseRepository.GetCourses().ToList();
        }
        
        public IEnumerable<Course> GetCoursesWithUsers()
        {
            return _courseRepository.GetCoursesWithUsers().ToList();
        }

        public IEnumerable<Course> GetCoursesForUser(Guid userId)
        {
            return _courseRepository.GetCoursesForUser(userId).ToList();
        }

        public Course GetCourse(int id)
        {
            return _courseRepository.GetCourse(id);
        }

        public void AddCourse(Course course)
        {
            _courseRepository.AddCourse(course);

            if (!_courseRepository.Save())
            {
                throw new Exception("Could not save Course to the database");
            }
        }

        public ActionStatus AddCourseForUser(Guid userId, int id)
        {
            var course = _courseRepository.GetCourseWithUsers(id);

            if (course == null)
            {
                return ActionStatus.CourseNotFound;
            }

            var userCourse = course.UserCourses.SingleOrDefault(n => n.CourseId == course.Id && n.UserId == userId);

            if (userCourse == null)
            {
                course.UserCourses.Add(new UserCourse { CourseId = course.Id, UserId = userId });

                if (!_courseRepository.Save())
                {
                    throw new Exception("Could not add Course for User to the database");
                }
            }

            return ActionStatus.Saved;
        }

        public ActionStatus DeleteCourse(int id)
        {
            var course = _courseRepository.GetCourse(id);

            if (course == null)
            {
                return ActionStatus.CourseNotFound;
            }

            _courseRepository.DeleteCourse(course);

            if (!_courseRepository.Save())
            {
                throw new Exception("Could not delete Course from the database");
            }

            return ActionStatus.Saved;
        }

        public ActionStatus DeleteCourseForUser(Guid userId, int id)
        {
            var course = _courseRepository.GetCourseWithUsers(id);

            if (course == null)
            {
                return ActionStatus.CourseNotFound;
            }

            var userCourse = course.UserCourses.SingleOrDefault(n => n.UserId == userId && n.CourseId == id);

            if (userCourse == null)
            {
                return ActionStatus.UserNotFound;
            }

            course.UserCourses.Remove(userCourse);

            if (!_courseRepository.Save())
            {
                throw new Exception("Could not delete User Course from the database");
            }

            return ActionStatus.Saved;
        }
    }
}
