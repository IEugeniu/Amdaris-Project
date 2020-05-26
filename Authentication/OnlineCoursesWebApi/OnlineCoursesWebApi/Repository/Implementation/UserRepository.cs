using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineCoursesWebApi.Contexts;
using OnlineCoursesWebApi.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWebApi.Repository
{
     public class UserRepository : Repository<User>, IUserRepository
     {
          OnlineCoursesDbContext _dbContext;
          IMapper _mapper;
          public UserRepository(OnlineCoursesDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
          {
               _dbContext = dbContext;
               _mapper = mapper;
          }

          public IEnumerable<User> GetAuthors()
          {
              return from u in _dbContext.Users
                    join ur in _dbContext.UserRole
                    on u.Id equals ur.UserId
                    join r in _dbContext.Roles
                    on ur.RoleId equals r.Id
                    where r.Name == "author"
                    select u;
          }

          public string GetRoleNameByUserId(long userId)
          {
               return _dbContext.UserRole.Join(_dbContext.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => new { UserId = ur.UserId, RoleName = r.Name })
                    .Where(x => x.UserId == userId).
                    Select(x => x.RoleName).FirstOrDefault();
          }


          public void UpdateUserRole(UserRole userRole)
          {
               UserRole userRoleToUpdate = _dbContext.UserRole.FirstOrDefault(x => x.UserId == userRole.UserId);

               if(userRoleToUpdate != null)
               {
                    _dbContext.UserRole.Remove(userRoleToUpdate);
                    _dbContext.UserRole.Add(userRole);
                    _dbContext.SaveChanges();
               }
          }
     }
}
