using OnlineCoursesWebApi.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWebApi.Models
{
     public class UserCourse
     {
          public long UserId { get; set; }
          public virtual User User { get; set; }
          public long CourseId { get; set; }
          public virtual Course Course { get; set; }
     }
}
