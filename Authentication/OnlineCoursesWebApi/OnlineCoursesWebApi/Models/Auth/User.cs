using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWebApi.Models.Auth
{
     public class User : IdentityUser<long>
     {
          [Required]
          [MaxLength(50)]
          public string FirstName { get; set; }
          [Required]
          [MaxLength(50)]
          public string LastName { get; set; }
          [MaxLength(50)]
          public string Experience { get; set; }
          public virtual List<UserCourse> UserCourses { get; set; }
          public virtual List<Comment> Commentarries { get; set; }
     }
}
