using OnlineCoursesWebApi.Models;
using OnlineCoursesWebApi.Models.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWebApi.Repository
{
     public interface IUserRepository : IRepository<User>
     {
          string GetRoleNameByUserId(long userId);
          public void UpdateUserRole(UserRole userRole);
          public IEnumerable<User> GetAuthors();
     }
}
