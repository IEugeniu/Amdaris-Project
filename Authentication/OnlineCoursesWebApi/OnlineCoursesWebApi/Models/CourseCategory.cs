using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWebApi.Models
{
     public class CourseCategory : Entity
     {
          [Required]
          [MaxLength(100)]
          public string CategoryName { get; set; }
          public virtual List<Course> Courses { get; set; }
     }
}
