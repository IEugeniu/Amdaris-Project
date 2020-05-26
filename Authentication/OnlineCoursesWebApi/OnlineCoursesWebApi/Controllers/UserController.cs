using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using OnlineCoursesWebApi.Dtos;
using OnlineCoursesWebApi.Models;
using OnlineCoursesWebApi.Models.Auth;
using OnlineCoursesWebApi.Repository;
using OnlineCoursesWebApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWebApi.Controllers
{
     [Authorize]
     [ApiController]
     [Route("api/users")]
     public class UserController : ControllerBase
     {
          private readonly IUserRepository _userRepository;
          private readonly IMapper _mapper;

          public UserController(IUserRepository userRoleRepository, IMapper mapper)
          {
               _userRepository = userRoleRepository;
               _mapper = mapper;
          }

          [Authorize(Roles = "admin")]
          [HttpGet]
          public IActionResult GetUsers()
          {
               var users = _userRepository.GetAll();

               if (users == null)
                    return NotFound();

               var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

               foreach (var item in usersDto)
               {
                    item.RoleName = _userRepository.GetRoleNameByUserId(item.Id);
               }


               return Ok(usersDto);
          }


          [HttpGet("{id}")]
          public IActionResult GetUser(long id)
          {
               var user = _userRepository.GetById(id);

               if (user == null)
                    return NotFound();

               var userDto = _mapper.Map<UserDto>(user);

               userDto.RoleName = _userRepository.GetRoleNameByUserId(id);

               return Ok(userDto);
          }


          [Authorize(Roles = "admin, author")]
          [Route("authors")]
          [HttpGet]
          public IActionResult GetAuthor()
          {
               var users = _userRepository.GetAuthors();

               if (users == null)
                    return NotFound();

               return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
          }

          [Authorize(Roles = "admin")]
          [HttpDelete("{id}")]
          public IActionResult DeleteUser(long id)
          {
               var user = _userRepository.GetById(id);

               if (user == null)
                    return NotFound();

               _userRepository.Delete(id);

               return NoContent();
          }

          [Authorize(Roles = "admin")]
          [HttpPut("role/{id}")]
          public IActionResult UpdateUserRole(long id, UpdateRoleDto role)
          {
               UserRole userRole = new UserRole { UserId = id, RoleId = role.RoleId };

               _userRepository.UpdateUserRole(userRole);

               return NoContent();
          }
     }
}
