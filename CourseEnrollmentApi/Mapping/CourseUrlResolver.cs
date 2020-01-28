using AutoMapper;
using CourseEnrollmentApi.DataAccess.Entities;
using CourseEnrollmentApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseEnrollmentApi.Mapping
{
    public class CourseUrlResolver : IValueResolver<Course, CourseModel, string>
    {
        private IHttpContextAccessor _httpContextAccessor;

        public CourseUrlResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Resolve(Course source, CourseModel destination, string destMember, ResolutionContext context)
        {
            var url = (IUrlHelper)_httpContextAccessor.HttpContext.Items["URL_HELPER"];
            return url.Link("CourseGet", new { id = source.Id });
        }
    }
}
