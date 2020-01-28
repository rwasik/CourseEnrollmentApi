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
    [Route("api/users")]
    [ValidateModel]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var users = _userService.GetUsers();
            
            return Ok(_mapper.Map<IEnumerable<UserModel>>(users));   
        }

        [HttpGet("{id}", Name = "UserGet")]
        public IActionResult Get(string id)
        {
            if (!Guid.TryParse(id, out var userId))
            {
                return BadRequest();
            }

            var user = _userService.GetUser(userId);

            if (user == null)
            {
                return NotFound($"User {id} was not found");
            }

            return Ok(_mapper.Map<UserModel>(user));
        }

        [HttpPost]
        public IActionResult Post([FromBody]UserModel model)
        {
            var user = _mapper.Map<User>(model);

            var status = _userService.AddUser(user);

            if (status == ActionStatus.DuplicatedUser)
            {
                return Conflict($"Email {model.Email} already exists");
            }

            var newUri = Url.Link("UserGet", new { id = user.Id });                
            return Created(newUri, _mapper.Map<UserModel>(user));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (!Guid.TryParse(id, out var userId))
            {
                return BadRequest();
            }

            var status = _userService.DeleteUser(userId);

            if (status == ActionStatus.UserNotFound)
            {
                return NotFound($"User {id} was not found");
            }

            return Ok();
        }
    }
}