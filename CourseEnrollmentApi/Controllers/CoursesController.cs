using System;
using System.Collections.Generic;
using AutoMapper;
using CourseEnrollmentApi.DataAccess.Entities;
using CourseEnrollmentApi.Filters;
using CourseEnrollmentApi.Models;
using CourseEnrollmentApi.Services.Services;
using CourseEnrollmentApi.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CourseEnrollmentApi.Controllers
{
    [ApiController]
    [ValidateModel]
    public class CoursesController : BaseController
    {
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CoursesController(ICourseService courseService, IUserService userService, IMapper mapper)
        {
            _courseService = courseService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("api/courses")]
        public IActionResult Get(bool includeEnrollmentsCount = false)
        {
            var courses = includeEnrollmentsCount ? _courseService.GetCoursesWithUsers() : _courseService.GetCourses();

            var items = includeEnrollmentsCount ?
                _mapper.Map<IEnumerable<CourseWithCountModel>>(courses) :
                _mapper.Map<IEnumerable<CourseModel>>(courses);

            return Ok(items);
        }

        [HttpGet("api/courses/{id}", Name = "CourseGet")]
        public IActionResult Get(int id)
        {            
            var course = _courseService.GetCourse(id);

            if (course == null)
            {
                return NotFound($"Course {id} was not found");
            }

            return Ok(_mapper.Map<CourseModel>(course));
        }

        [HttpPost("api/courses")]
        public IActionResult Post([FromBody]CourseModel model)
        {
            var course = _mapper.Map<Course>(model);

            _courseService.AddCourse(course);

            var newUri = Url.Link("CourseGet", new { id = course.Id });
            return Created(newUri, _mapper.Map<CourseModel>(course));
        }

        [HttpDelete("api/courses/{id}")]
        public IActionResult Delete(int id)
        {
            var status = _courseService.DeleteCourse(id);

            if (status == ActionStatus.CourseNotFound)
            {
                return NotFound($"Course {id} was not found");
            }

            return Ok();
        }

        [HttpGet("api/users/{userId}/courses")]
        public IActionResult Get(string userId)
        {
            if (!Guid.TryParse(userId, out var parsedUserId))
            {
                return BadRequest();
            }

            var courses = _courseService.GetCoursesForUser(parsedUserId);

            return Ok(_mapper.Map<IEnumerable<CourseModel>>(courses)); 
        }

        [HttpDelete("api/users/{userId}/courses/{id}")]
        public IActionResult Delete(string userId, int id)
        {
            if (!Guid.TryParse(userId, out var parsedUserId))
            {
                return BadRequest();
            }

            var status = _courseService.DeleteCourseForUser(parsedUserId, id);

            if (status == ActionStatus.CourseNotFound)
            {
                return NotFound($"Course {id} was not found");
            }
            else if (status == ActionStatus.UserNotFound)
            {
                return NotFound($"User {userId} assigned to the Course was not found");
            }

            return Ok();
        }

        [HttpPut("api/users/{userId}/courses")]
        public IActionResult Put(string userId, [FromBody]EnrollCourseModel model)
        {
            if (!Guid.TryParse(userId, out var parsedUserId))
            {
                return BadRequest();
            }

            var user = _userService.GetUser(parsedUserId);

            if (user == null)
            {
                return NotFound($"User {userId} was not found");
            }

            var status = _courseService.AddCourseForUser(user.Id, model.CourseId);

            if (status == ActionStatus.CourseNotFound)
            {
                return NotFound($"Course {model.CourseId} was not found");
            }

            var courses = _courseService.GetCoursesForUser(user.Id);
            return Ok(_mapper.Map<IEnumerable<CourseModel>>(courses));
        }
    }
}