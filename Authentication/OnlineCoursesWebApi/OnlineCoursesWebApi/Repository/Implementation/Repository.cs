using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineCoursesWebApi.Contexts;
using OnlineCoursesWebApi.Infrastructure.Exntensions;
using OnlineCoursesWebApi.Models;
using OnlineCoursesWebApi.Paginating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWebApi.Repository
{
     public class Repository<T> : IRepository<T> where T : class
     {
          OnlineCoursesDbContext _dbContext;
          DbSet<T> _dbSet;
          IMapper _mapper;

          public Repository(OnlineCoursesDbContext dbContext, IMapper mapper)
          {
               _dbContext = dbContext;
               _dbSet = _dbContext.Set<T>();
               _mapper = mapper;
          }

          public void Add(T t)
          {
               _dbSet.Add(t);
               _dbContext.SaveChanges();
          }

          public void Delete(long id)
          {
               T t = _dbSet.Find(id);

               if (t == null)
                    throw new Exception("Entity not found");

               _dbSet.Remove(t);
               _dbContext.SaveChanges();
          }

          public IEnumerable<T> GetAll()
          {
               return _dbSet;
          }

          public T GetById(long id)
          {
               return _dbSet.Find(id);
          }

          public async Task<PaginatedResult<TDto>> GetPagedData<TEntity, TDto>(PagedRequest pagedRequest) where TEntity : Entity
                                                                                            where TDto : class
          {
               return await _dbContext.Set<TEntity>().CreatePaginatedResultAsync<TEntity, TDto>(pagedRequest, _mapper);
          }

          public void Update(T t)
          {
               _dbSet.Attach(t);
               try
               {
                    _dbContext.Entry(t).State = EntityState.Modified;
                    _dbContext.SaveChanges();
               }
               catch(Exception e)
               {
                    
               }
          }
     }
}
