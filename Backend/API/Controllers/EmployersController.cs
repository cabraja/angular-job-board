using API.Helpers.DTO;
using API.Helpers.Exceptions;
using API.Helpers.QueryModels;
using API.Interfaces.Repos;
using DataAccess;
using DataAccess.Auth;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployersController : ControllerBase
    {
        private IEmployerRepo _repo;
        private UserManager<AppUser> _userManager;

        public EmployersController(IEmployerRepo repo,UserManager<AppUser> userManager)
        {
            _repo = repo;
            _userManager = userManager; 
        }


        // GET: api/<EmployersController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] EmployerQueryModel queryModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var employers = await _repo.GetEmployersAsync(queryModel);
                return Ok(employers);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

        }

        // GET: api/<EmployersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var employer = await _repo.GetSingleEmployerAsync(id);
                return Ok(employer);
            }
            catch(EntityNotFoundException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        // PATCH api/<EmployersController>/5
        [Authorize(Roles = "Employer")]
        [HttpPatch("{employerId}")]
        public async Task<IActionResult> PatchEmployer(int employerId, [FromBody] EditEmployerDTO editEmployerDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var appUser = await _userManager.FindByNameAsync(User.Identity.Name);

                var editedEmployer = await _repo.EditEmployer(editEmployerDTO, appUser.Id, employerId);

                return Ok(editedEmployer);
            }
            catch(ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(NotEntityOwnerException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

    }
}
