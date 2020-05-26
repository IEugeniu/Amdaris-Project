using OnlineCoursesWebApi.Models;
using OnlineCoursesWebApi.Paginating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWebApi.Repository
{
     public interface IRepository<T> where T : class
     {
          void Add(T t);
          T GetById(long id);
          IEnumerable<T> GetAll();
          void Delete(long id);
          public void Update(T t);
          Task<PaginatedResult<TDto>> GetPagedData<TEntity, TDto>(PagedRequest pagedRequest) where TEntity : Entity
                                                                                             where TDto : class;
     }
}
