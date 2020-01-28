using AutoMapper;
using CourseEnrollmentApi.DataAccess.Entities;
using CourseEnrollmentApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseEnrollmentApi.Mapping
{
    public class UserUrlResolver : IValueResolver<User, UserModel, string>
    {
        private IHttpContextAccessor _httpContextAccessor;

        public UserUrlResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Resolve(User source, UserModel destination, string destMember, ResolutionContext context)
        {
            var url = (IUrlHelper)_httpContextAccessor.HttpContext.Items["URL_HELPER"];
            return url.Link("UserGet", new { id = source.Id });
        }
    }
}
