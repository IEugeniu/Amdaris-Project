using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWebApi.Profiles
{
     public class CourseProfile : Profile
     {
          public CourseProfile()
          {
               CreateMap<Models.Course, Dtos.CourseDto>()
                    .ForMember(dest => dest.Category, act => act.MapFrom(src => src.CourseCategory.CategoryName))
                    .ForMember(dest => dest.AuthorName, act => act.MapFrom(src => src.Author.UserName));
          }
     }
}
