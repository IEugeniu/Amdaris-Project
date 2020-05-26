using OnlineCoursesWebApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWebApi.Repository
{
     public interface ICourseRepository : IRepository<Course>
     {
          IEnumerable GetByName(string name);
          IEnumerable GetByCategory(string category);
     }
}
