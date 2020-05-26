﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWebApi.Dtos
{
     public class UserDto
     {
          public long Id { get; set; }
          public string FirstName { get; set; }
          public string LastName { get; set; }
          public string Experience { get; set; }
          public string Username { get; set; }
          public string Email { get; set; }
          public string RoleName { get; set; }
     }
}
