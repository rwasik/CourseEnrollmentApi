using System;
using AutoMapper;
using CourseEnrollmentApi.DataAccess.Entities;
using CourseEnrollmentApi.Models;

namespace CourseEnrollmentApi.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserModel, User>()
                 .ForMember(c => c.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            
            CreateMap<User, UserModel>()
                .ForMember(c => c.Url, opt => opt.MapFrom<UserUrlResolver>());
        }
    }
}
