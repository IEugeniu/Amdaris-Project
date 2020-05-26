using AutoMapper;
using OnlineCoursesWebApi.Contexts;
using OnlineCoursesWebApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWebApi.Repository
{
     public class CourseRepository : Repository<Course>, ICourseRepository
     {
          OnlineCoursesDbContext _dbContext;
          IMapper _mapper;
          public CourseRepository(OnlineCoursesDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
          {
               _dbContext = dbContext;
          }

          public IEnumerable GetByName(string name)
          {
               return _dbContext.Course.Where(c => c.Name.Contains(name));
          }

          public IEnumerable GetByCategory(string category)
          {
               return _dbContext.Course.Where(c => c.CourseCategory.CategoryName.Contains(category));
          }
     }
}
