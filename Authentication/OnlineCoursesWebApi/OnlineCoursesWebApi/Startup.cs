using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OnlineCoursesWebApi.Contexts;
using OnlineCoursesWebApi.Infrastructure.Exntensions;
using OnlineCoursesWebApi.Models.Auth;
using OnlineCoursesWebApi.Repository;
using OnlineCoursesWebApi.Services.Implementation;
using OnlineCoursesWebApi.Services.Interfaces;

namespace OnlineCoursesWebApi
{
     public class Startup
     {
          public Startup(IConfiguration configuration)
          {
               Configuration = configuration;
          }

          public IConfiguration Configuration { get; }

          public void ConfigureServices(IServiceCollection services)
          {
               services.AddCors(options => options.AddPolicy("Cors", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
               services.AddDbContext<OnlineCoursesDbContext>(item => item.UseSqlServer(Configuration.GetConnectionString("CourseDBConnection")));
               services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
               services.AddScoped<ICourseRepository, CourseRepository>();
               services.AddScoped<IUserRepository, UserRepository>();
               services.AddScoped<IImageService, ImageService>();
               services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
               services.AddIdentity<User, Role>(options => { options.Password.RequiredLength = 8; })
               .AddEntityFrameworkStores<OnlineCoursesDbContext>();

               var authOptions = services.ConfigureAuthOptions(Configuration);

               services.AddJwtAuthentication(authOptions);
               services.AddControllers(options => { options.Filters.Add(new AuthorizeFilter()); });
          }

          public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
          {
               if (env.IsDevelopment())
               {
                    app.UseDeveloperExceptionPage();
               }

               app.UseStaticFiles();

               app.UseCors("Cors");

               app.UseRouting();
               app.UseAuthentication();
               app.UseAuthorization();

               

               app.UseEndpoints(endpoints =>
               {
                    endpoints.MapControllers();
               });
          }
     }
}
