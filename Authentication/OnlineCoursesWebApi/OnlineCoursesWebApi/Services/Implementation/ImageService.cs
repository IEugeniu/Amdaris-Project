using Microsoft.AspNetCore.Http;
using OnlineCoursesWebApi.Services.Interfaces;
using System.IO;
using System.Net.Http.Headers;

namespace OnlineCoursesWebApi.Services.Implementation
{
     public class ImageService : IImageService
     {
          public void SaveImage(IFormFile file)
          {
               var folderName = Path.Combine("wwwroot", "CourseImages");
               var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

               if (file.Length > 0)
               {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                         file.CopyTo(stream);
                    }
               }
          }
     }
}
