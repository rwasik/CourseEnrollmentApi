using System;
using System.Collections.Generic;
using CourseEnrollmentApi.DataAccess.Entities;
using CourseEnrollmentApi.Shared.Enums;

namespace CourseEnrollmentApi.Services.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        User GetUser(Guid id);
        ActionStatus AddUser(User user);
        ActionStatus DeleteUser(Guid id);
    }
}
