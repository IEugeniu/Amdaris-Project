using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWebApi.Dtos
{
     public class CourseDto
     {
          public long Id { get; set; }
          public string Name { get; set; }
          public string Category { get; set; }
          public string Description { get; set; }
          public string ImageLink { get; set; }
          public string VideoLink { get; set; }
          public string AuthorName { get; set; }
     }
}
