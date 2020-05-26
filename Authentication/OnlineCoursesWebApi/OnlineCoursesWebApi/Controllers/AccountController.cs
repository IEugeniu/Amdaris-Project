using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineCoursesWebApi.Dtos;
using OnlineCoursesWebApi.Infrastructure.Configuration;
using OnlineCoursesWebApi.Models.Auth;
using OnlineCoursesWebApi.Repository;
using OnlineCoursesWebApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineCoursesWebApi.Controllers
{
     [AllowAnonymous]
     [Route("api/[controller]")]
     [ApiController]
     public class AccountController : ControllerBase
     {
          private readonly AuthOptions _authenticationOptions;
          private readonly SignInManager<User> _signInManager;
          private readonly UserManager<User> _userManager;
          public readonly IUserRepository _userRoleRepository;
          private IMapper _mapper;

          public AccountController(IOptions<AuthOptions> authenticationOptions, SignInManager<User> signInManager, UserManager<User> userManager, IMapper mapper, IUserRepository userRoleRepository)
          {
               _authenticationOptions = authenticationOptions.Value;
               _signInManager = signInManager;
               _userManager = userManager;
               _userRoleRepository = userRoleRepository;
               _mapper = mapper;
          }

          [HttpPost("login")]
          public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
          {
               var user = await _userManager.FindByNameAsync(userForLoginDto.Username);
               var checkingPasswordResult = await _signInManager.PasswordSignInAsync(userForLoginDto.Username, userForLoginDto.Password, false, false);

               if (checkingPasswordResult.Succeeded)
               {
                    var role = await _userManager.GetRolesAsync(user);
                    IdentityOptions options = new IdentityOptions();

                    var signinCredentials = new SigningCredentials(_authenticationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);
                    var jwtSecurityToken = new JwtSecurityToken(
                         issuer: _authenticationOptions.Issuer,
                         audience: _authenticationOptions.Audience,
                         claims: new List<Claim>()
                         {
                              new Claim(options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                         },
                         expires: DateTime.Now.AddDays(30),
                         signingCredentials: signinCredentials
                    );

                    var tokenHandler = new JwtSecurityTokenHandler();

                    var encodedToken = tokenHandler.WriteToken(jwtSecurityToken);

                    return Ok(new { AccessToken = encodedToken });
               }

               return Unauthorized();
          }

          [HttpPost("{roleName}")]
          public async Task<IActionResult> Register(RegisterUserDto model, string roleName)
          {
               if (!ModelState.IsValid)
               {
                    return BadRequest(model);
               }
               User user = _mapper.Map<User>(model);
               var result = await _userManager.CreateAsync(user, model.Password);
               if (result.Succeeded)
               {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    await _userManager.AddToRoleAsync(user, roleName);
                    return Ok(result);
               }
               else
               {
                    return BadRequest();
               }
          }
     }
}
