using Microsoft.AspNetCore.Http;
using OnlineCoursesWebApi.Models.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWebApi.Models
{
     public class Course : Entity
     {
          [Required]
          [MaxLength(100)]
          public string Name { get; set; }
          [Required]
          public long CourseCategoryId { get; set; }
          public virtual CourseCategory CourseCategory { get; set; }
          [MaxLength(400)]
          public string Description { get; set; }
          public string ImageLink { get; set; }
          [NotMapped]
          public Guid ImageName { get; set; }
          [Required]
          public string VideoLink { get; set; }
          [Required]
          public long AuthorId { get; set; }
          public virtual User Author { get; set; }
          public virtual List<Comment> Commentaries { get; set; }
          public virtual List<UserCourse> UserCourses { get; set; }
     }
}
