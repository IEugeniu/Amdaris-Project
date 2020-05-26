using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineCoursesWebApi.Models;
using OnlineCoursesWebApi.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWebApi.Contexts
{
     public class OnlineCoursesDbContext : IdentityDbContext<User, Role, long, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
     {

          private const string connectionString = "Data Source=.;Initial Catalog=OnlineCoursesWebApi;Integrated Security=True; MultipleActiveResultSets=true";

          public OnlineCoursesDbContext()
          {
          }

          public OnlineCoursesDbContext(DbContextOptions<OnlineCoursesDbContext> options)
               : base(options)
          {
          }

          protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          {
               optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectionString);
          }

          public DbSet<Course> Course { get; set; }

          public DbSet<Comment> Comment { get; set; }

          public DbSet<CourseCategory> CourseCategory { get; set; }

          public DbSet<UserCourse> UserCourse { get; set; }

          public DbSet<UserRole> UserRole { get; set; }

          protected override void OnModelCreating(ModelBuilder modelBuilder)
          {
               base.OnModelCreating(modelBuilder);

               modelBuilder.ApplyConfiguration(new UserCourseConfig());
               modelBuilder.ApplyConfiguration(new CommentConfig());
               //modelBuilder.ApplyConfiguration(new CourseCategoryConfig());
               //modelBuilder.ApplyConfiguration(new CourseConfig());
               //modelBuilder.ApplyConfiguration(new UserConfig());

               ApplyIdentityMapConfiguration(modelBuilder);

               modelBuilder.Entity<Role>().HasData(
                    new Role { Id = 1, Name = "admin", NormalizedName = "ADMIN" },
                    new Role { Id = 2, Name = "author", NormalizedName = "AUTHOR" },
                    new Role { Id = 3, Name = "student", NormalizedName = "STUDENT" }
                    );

               modelBuilder.Entity<UserRole>().HasData(
                    new UserRole { UserId = 1, RoleId = 1},
                    new UserRole { UserId = 2, RoleId = 2 });

               modelBuilder.Entity<User>().HasData(
                    new User { Id = 1, FirstName = "Eugeniu", LastName = "Ignat", Experience = "Amdaris", UserName = "eugeniu"},
                    new User { Id = 2, FirstName = "Vlad", LastName = "Sirbu", Experience = "Microsoft", UserName = "vlad"});

               modelBuilder.Entity<CourseCategory>().HasData(
                    new CourseCategory { Id = 1, CategoryName = "Mobile" },
                    new CourseCategory { Id = 2, CategoryName = "Web" },
                    new CourseCategory { Id = 3, CategoryName = "Testing" },
                    new CourseCategory { Id = 4, CategoryName = "Game" },
                    new CourseCategory { Id = 5, CategoryName = "Database" },
                    new CourseCategory { Id = 6, CategoryName = "Artificial Intelligence" });

               modelBuilder.Entity<Course>().HasData(
                    new Course { Id = 1, Name = "ASP.NET Core", CourseCategoryId = 2, ImageLink = "https://localhost:44305/CourseImages/1b4bd51b-5ff5-5da8-884b-63f6d59fa9d1.jpg", VideoLink = "", AuthorId = 1 },
                    new Course { Id = 2, Name = "Entity Framework", CourseCategoryId = 5, ImageLink = "https://localhost:44305/CourseImages/5b19461f-c3d2-8ba5-766c-ca0008f4f42c.jpg", VideoLink = "", AuthorId = 1 },
                    new Course { Id = 3, Name = "Android", CourseCategoryId = 1, ImageLink = "https://localhost:44305/CourseImages/f7745c30-7511-1a60-1e65-078c924ed090.jpg", VideoLink = "", AuthorId = 1 },
                    new Course { Id = 4, Name = "Object recognition", CourseCategoryId = 6, ImageLink = "https://localhost:44305/CourseImages/c65e28b7-7d9f-d349-f330-0e6dd5fa6f70.jpg", VideoLink = "", AuthorId = 1 });
          }

          private void ApplyIdentityMapConfiguration(ModelBuilder modelBuilder)
          {
               modelBuilder.Entity<UserClaim>().ToTable("UserClaims", SchemaConsts.Auth);
               modelBuilder.Entity<UserLogin>().ToTable("UserLogins", SchemaConsts.Auth);
               modelBuilder.Entity<UserToken>().ToTable("UserRoles", SchemaConsts.Auth);
               modelBuilder.Entity<Role>().ToTable("Roles", SchemaConsts.Auth);
               modelBuilder.Entity<RoleClaim>().ToTable("RoleClaims", SchemaConsts.Auth);
               modelBuilder.Entity<UserRole>().ToTable("UserRole", SchemaConsts.Auth);
               modelBuilder.Entity<User>().ToTable("Users", SchemaConsts.Auth);
          }

          /*internal class CourseConfig : IEntityTypeConfiguration<Course>
          {
               public void Configure(EntityTypeBuilder<Course> builder)
               {
                    builder.HasKey(x => x.Id);

                    builder.Property(x => x.Name)
                        .IsRequired()
                        .HasMaxLength(100);

                    builder.HasOne(x => x.CourseCategory);

                    builder.Property(x => x.Description)
                        .HasMaxLength(400);

                    builder.Property(x => x.VideoLink)
                        .IsRequired();

                    builder.HasOne(x => x.Author);

                    builder.HasMany(x => x.UserCourses);
                    //builder.Property(x => x.UserCourses);

               }
          }*/

          /*public class UserConfig : IEntityTypeConfiguration<User>
          {
               public void Configure(EntityTypeBuilder<User> builder)
               {
                    builder.HasKey(x => x.Id);

                    builder.Property(x => x.FirstName)
                        .IsRequired()
                        .HasMaxLength(50);

                    builder.Property(x => x.LastName)
                        .IsRequired()
                        .HasMaxLength(50);

                    builder.Property(x => x.Experience)
                        .HasMaxLength(100)
                        .HasDefaultValue("No experience");

                    //builder.HasOne(x => x.UserRole);

                    builder.HasMany(x => x.UserCourses);

                    builder.HasMany(x => x.Commentarries);
               }
          }*/

          public class UserCourseConfig : IEntityTypeConfiguration<UserCourse>
          {
               public void Configure(EntityTypeBuilder<UserCourse> builder)
               {
                    builder.HasKey(uc => new { uc.UserId, uc.CourseId });
                    builder.HasOne(x => x.User)
                         .WithMany(x => x.UserCourses)
                         .HasForeignKey(x => x.UserId)
                         .OnDelete(DeleteBehavior.NoAction);

                    builder.HasOne(x => x.Course)
                         .WithMany(x => x.UserCourses)
                         .HasForeignKey(x => x.CourseId)
                         .OnDelete(DeleteBehavior.NoAction);
               }
          }

          public class CommentConfig : IEntityTypeConfiguration<Comment>
          {
               public void Configure(EntityTypeBuilder<Comment> builder)
               {
                    builder.HasKey(uc => new { uc.UserId, uc.CourseId });
                    builder.HasOne(x => x.User)
                         .WithMany(x => x.Commentarries)
                         .HasForeignKey(x => x.UserId)
                         .OnDelete(DeleteBehavior.NoAction);

                    builder.HasOne(x => x.Course)
                         .WithMany(x => x.Commentaries)
                         .HasForeignKey(x => x.CourseId)
                         .OnDelete(DeleteBehavior.NoAction);
               }
          }

          /*public class CourseCategoryConfig : IEntityTypeConfiguration<CourseCategory>
          {
               public void Configure(EntityTypeBuilder<CourseCategory> builder)
               {
                    builder.HasKey(x => x.Id);

                    builder.HasMany(x => x.Courses)
                         .WithOne(x => x.CourseCategory)
                         .OnDelete(DeleteBehavior.Restrict);
               }
          }*/
     }
}
