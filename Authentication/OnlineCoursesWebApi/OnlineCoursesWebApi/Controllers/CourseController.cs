using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineCoursesWebApi.Dtos;
using OnlineCoursesWebApi.Models;
using OnlineCoursesWebApi.Paginating;
using OnlineCoursesWebApi.Repository;
using OnlineCoursesWebApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Web;

namespace OnlineCoursesWebApi.Controllers
{
     [Authorize]
     [ApiController]
     [Route("api/courses")]
     public class CourseController : ControllerBase
     {
          private readonly ICourseRepository _courseRepository;
          private readonly IImageService _imageService;
          private readonly IMapper _mapper;

          public CourseController(ICourseRepository courseRepository, IImageService imageService, IMapper mapper)
          {
               _courseRepository = courseRepository;
               _imageService = imageService;
               _mapper = mapper;
          }

          [AllowAnonymous]
          [HttpPost("PaginatedSearch")]
          public IActionResult GetPagedBooks(PagedRequest pagedRequest)
          {
               var pagedBooksDto = _courseRepository.GetPagedData<Course, CourseDto>(pagedRequest);
               return Ok(pagedBooksDto);
          }

          [HttpGet]
          public IActionResult GetCourses()
          {
               var courses = _courseRepository.GetAll();

               if (courses == null)
                    return NotFound();

               return Ok(_mapper.Map<IEnumerable<CourseDto>>(courses));
          }

          [HttpGet("{id}")]
          public IActionResult GetCourse(long id)
          {
               var course = _courseRepository.GetById(id);

               if (course == null)
                    return NotFound();

               return Ok(_mapper.Map<CourseDto>(course));
          }

          [HttpGet("searchByName/{name}")]
          public IActionResult GetCourseByName(string name)
          {
               var courses = _courseRepository.GetByName(name);

               if (courses == null)
                    return NotFound();

               return Ok(_mapper.Map<IEnumerable<CourseDto>>(courses));
          }

          [HttpGet("searchByCategory/{category}")]
          public IActionResult GetCourseByCategory(string category)
          {
               var courses = _courseRepository.GetByCategory(category);

               if (courses == null)
                    return NotFound();

               return Ok(_mapper.Map<IEnumerable<CourseDto>>(courses));
          }

          [Authorize(Roles = "admin, author")]
          [HttpPost]
          public IActionResult CreateCourse(Course course)
          {
               if (course == null)
               {
                    return NotFound();
               }

               course.ImageLink = "https://localhost:44305/CourseImages/" + course.ImageName + ".jpg";

               _courseRepository.Add(course);

               return Ok(course);
          }

          [Authorize(Roles = "admin, author")]
          [Route("image")]
          [HttpPost, DisableRequestSizeLimit]
          public IActionResult UploadedFile()
          {
               var file = Request.Form.Files[0];

               _imageService.SaveImage(file);

               return Ok();
          }

          [Authorize(Roles = "admin, author")]
          [HttpDelete("{id}")]
          public IActionResult DeleteCourse(long id)
          {
               var course = _courseRepository.GetById(id);

               if (course == null)
                    return NotFound();

               _courseRepository.Delete(id);

               return NoContent();
          }

          [Authorize(Roles = "admin, author")]
          [HttpPut("{id}")]
          public IActionResult UpdateCourse(long id, Course course)
          {
               var courseToUpdate = _courseRepository.GetById(id);

               if (course == null || courseToUpdate == null)
                    return NotFound();

               courseToUpdate.Name = course.Name;
               courseToUpdate.CourseCategoryId = course.CourseCategoryId;
               courseToUpdate.Description = course.Description;
               courseToUpdate.ImageName = course.ImageName;
               courseToUpdate.ImageLink = "https://localhost:44305/CourseImages/" + course.ImageName + ".jpg";
               courseToUpdate.VideoLink = course.VideoLink;
               courseToUpdate.AuthorId = course.AuthorId;

               _courseRepository.Update(courseToUpdate);

               return NoContent();
          }
     }
}
