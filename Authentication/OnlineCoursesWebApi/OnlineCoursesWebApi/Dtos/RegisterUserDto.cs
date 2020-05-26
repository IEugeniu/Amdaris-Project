using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWebApi.Dtos
{
     public class RegisterUserDto
     {
          public string FirstName { get; set; }
          public string LastName { get; set; }
          public string Experience { get; set; }
          public string Username { get; set; }
          [EmailAddress(ErrorMessage = "Invalid Email Address")]
          public string Email { get; set; }
          public string Password { get; set; }
     }
}
