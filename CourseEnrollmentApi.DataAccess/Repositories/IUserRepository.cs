using System;
using System.Linq;
using CourseEnrollmentApi.DataAccess.Entities;

namespace CourseEnrollmentApi.DataAccess.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> GetUsers();
        User GetUser(Guid id);
        User GetUser(string email);
        void AddUser(User user);
        void DeleteUser(User user);
        bool Save();
    }
}
