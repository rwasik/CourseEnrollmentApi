using System;
using System.Collections.Generic;
using System.Linq;
using CourseEnrollmentApi.DataAccess.Entities;
using CourseEnrollmentApi.DataAccess.Repositories;
using CourseEnrollmentApi.Shared.Enums;

namespace CourseEnrollmentApi.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetUsers().ToList();
        }

        public User GetUser(Guid id)
        {
            return _userRepository.GetUser(id);
        }

        public ActionStatus AddUser(User user)
        {
            var existingUser = _userRepository.GetUser(user.Email);

            if (existingUser != null)
            {
                return ActionStatus.DuplicatedUser;
            }

            _userRepository.AddUser(user);

            if (!_userRepository.Save())
            {
                throw new Exception("Could not save User to the database");
            }

            return ActionStatus.Saved;
        }

        public ActionStatus DeleteUser(Guid id)
        {
            var user = _userRepository.GetUser(id);

            if (user == null)
            {
                return ActionStatus.UserNotFound;
            }

            _userRepository.DeleteUser(user);

            if (!_userRepository.Save())
            {
                throw new Exception("Could not delete User from the database");
            }

            return ActionStatus.Saved;
        }
    }
}
