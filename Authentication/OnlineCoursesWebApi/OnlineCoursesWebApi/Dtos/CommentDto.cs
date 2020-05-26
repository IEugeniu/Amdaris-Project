using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWebApi.Dtos
{
     public class CommentDto
     {
          public string UserName { get; set; }
          public string CourseName { get; set; }
          public string Commentary { get; set; }
     }
}
