using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using OnlineCoursesWebApi.Dtos;
using OnlineCoursesWebApi.Models;
using OnlineCoursesWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWebApi.Controllers
{
     [Authorize]
     [ApiController]
     [Route("api/categories")]
     public class CategoryController : ControllerBase
     {
          private readonly IRepository<CourseCategory> _categoryRepository;
          private readonly IMapper _mapper;

          public CategoryController(IRepository<CourseCategory> categoryRepository, IMapper mapper)
          {
               _categoryRepository = categoryRepository;
               _mapper = mapper;
          }

          public IActionResult GetCategories()
          {
               var categories = _categoryRepository.GetAll();

               if (categories == null)
                    return BadRequest();

               return Ok(_mapper.Map<IEnumerable<CourseCategoryDto>>(categories));
          }

          [Authorize(Roles = "admin")]
          [HttpPost]
          public IActionResult CreateCategory(CourseCategory category)
          {
               if (category == null)
               {
                    return NotFound();
               }
               _categoryRepository.Add(category);

               return Ok(category);
          }

          [Authorize(Roles = "admin")]
          [HttpDelete("{id}")]
          public IActionResult DeleteCategory(long id)
          {
               var category = _categoryRepository.GetById(id);

               if (category == null)
                    return NotFound();

               _categoryRepository.Delete(id);

               return NoContent();
          }

          [Authorize(Roles = "admin")]
          [HttpPut("{id}")]
          public IActionResult UpdateCategory(long id, CourseCategory category)
          {
               var categoryToUpdate = _categoryRepository.GetById(id);

               if (category == null || categoryToUpdate == null)
                    return NotFound();

               categoryToUpdate.CategoryName = category.CategoryName;

               _categoryRepository.Update(categoryToUpdate);

               return NoContent();
          }
     }
}
