using System;
using System.Linq;
using CourseEnrollmentApi.DataAccess.Context;
using CourseEnrollmentApi.DataAccess.Entities;

namespace CourseEnrollmentApi.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IQueryable<User> GetUsers()
        {
            return _dataContext.Users;
        }

        public User GetUser(Guid id)
        {
            return GetUsers().SingleOrDefault(u => u.Id == id);
        }

        public User GetUser(string email)
        {
            return GetUsers().SingleOrDefault(u => u.Email.ToLower() == email.ToLower());
        }

        public void AddUser(User user)
        {
            _dataContext.Users.Add(user);
        }

        public void DeleteUser(User user)
        {
            _dataContext.Remove(user);
        }

        public bool Save()
        {
            return _dataContext.SaveChanges() > 0;
        }
    }
}
