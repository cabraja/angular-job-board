using API.Helpers.DTO.Auth;
using API.Interfaces.Auth;
using API.Services;
using DataAccess.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        // POST api/<AccountController>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == login.Email.ToLower());

                if(user == null) return Unauthorized("Invalid email address.");

                var result = await _signInManager.CheckPasswordSignInAsync(user,login.Password, false);

                if(!result.Succeeded) return Unauthorized("Incorrect password.");

                var role = await _userManager.GetRolesAsync(user);

                return Ok(new NewUserDTO
                {
                    Email = login.Email,
                    Token = _tokenService.CreateToken(user, role[0])
                });
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        // POST api/<AccountController>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest(ModelState);

                var appUser = new AppUser
                {
                    UserName = registerDTO.Email,
                    Email = registerDTO.Email,

                };
                var createdUser = await _userManager.CreateAsync(appUser, registerDTO.Password);

                if (createdUser.Succeeded)
                {
                    IdentityResult roleResult;
                    if(registerDTO.IsEmployer)
                    {
                       roleResult = await _userManager.AddToRoleAsync(appUser, "Employer");
                    }
                    else
                    {
                        roleResult = await _userManager.AddToRoleAsync(appUser, "Regular");
                    }

                    if(roleResult.Succeeded)
                    {
                        return Ok(new NewUserDTO
                        {
                            Email = appUser.Email,
                            Token = _tokenService.CreateToken(appUser, registerDTO.IsEmployer ? "Employer" : "Regular")
                        });
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
