using API.Helpers.DTO.Auth;
using DataAccess.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        // POST api/<AccountController>
        [HttpPost]
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
                        return Ok("User created.");
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
