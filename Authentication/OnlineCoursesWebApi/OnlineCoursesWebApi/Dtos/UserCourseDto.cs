using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWebApi.Dtos
{
     public class UserCourseDto
     {
          public long UserId { get; set; }
          public long CourseId { get; set; }
     }
}
