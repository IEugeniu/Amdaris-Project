using AutoMapper;
using OnlineCoursesWebApi.Dtos;
using OnlineCoursesWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWebApi.Profiles
{
     public class CategoryProfile : Profile
     {
          public CategoryProfile()
          {
               CreateMap<CourseCategory, CourseCategoryDto>();
          }
     }
}
