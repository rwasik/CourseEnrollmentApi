using AutoMapper;
using CourseEnrollmentApi.DataAccess.Entities;
using CourseEnrollmentApi.Models;

namespace CourseEnrollmentApi.Mapping
{
    public class CourseMappingProfile : Profile
    {
        public CourseMappingProfile()
        {
            CreateMap<CourseModel, Course>();

            CreateMap<Course, CourseModel>()
                .ForMember(c => c.Url, opt => opt.MapFrom<CourseUrlResolver>());

            CreateMap<Course, CourseWithCountModel>()
                .ForMember(c => c.Url, opt => opt.MapFrom<CourseUrlResolver>())
                .ForMember(c => c.EnrollmentsCount, opt => opt.MapFrom(src => src.UserCourses.Count));
        }
    }
}
