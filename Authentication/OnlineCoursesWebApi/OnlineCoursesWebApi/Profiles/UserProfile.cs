using AutoMapper;
using OnlineCoursesWebApi.Dtos;
using OnlineCoursesWebApi.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWebApi.Profiles
{
     public class UserProfile : Profile
     {
          public UserProfile()
          {
               CreateMap<RegisterUserDto, User>();
               CreateMap<User, UserDto>();
          }
     }
}
