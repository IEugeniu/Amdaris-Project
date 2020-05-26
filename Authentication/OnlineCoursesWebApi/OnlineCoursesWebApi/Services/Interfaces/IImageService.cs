using Microsoft.AspNetCore.Http;

namespace OnlineCoursesWebApi.Services.Interfaces
{
     public interface IImageService
     {
          public void SaveImage(IFormFile file);
     }
}
