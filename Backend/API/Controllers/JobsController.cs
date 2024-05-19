using API.Helpers.DTO;
using API.Helpers.Exceptions;
using API.Helpers.QueryModels;
using API.Interfaces.Repos;
using API.Repos;
using DataAccess.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private IJobRepo _repo;
        public UserManager<AppUser> _userManager;

        public JobsController(IJobRepo repo, UserManager<AppUser> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        // GET: api/<JobsController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] JobQueryModal queryModal)
        {
            if(!ModelState.IsValid) { return  BadRequest(); };

            try
            {
                var jobs = await _repo.GetJobsAsync(queryModal);
                return Ok(jobs);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // GET api/<JobsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid) { return BadRequest(); };

            try
            {
                var job = await _repo.GetSingleJobAsync(id);
                return Ok(job);
            }
            catch (EntityNotFoundException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // POST api/<JobsController>
        [HttpPost]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> Post([FromBody] AddJobDTO body)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var job = await _repo.CreateJob(body);
                return StatusCode(201, job);
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

        // PATCH api/<JobsController>/5
        [HttpPatch("{id}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> Patch(int id, [FromBody] EditJobDTO editJobDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                if (appUser == null)
                {
                    return BadRequest("User not found.");
                }
                var updatedJob = await _repo.UpdateJob(id, editJobDTO, appUser);
                return Ok(updatedJob);
            }
            catch(EntityNotFoundException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch(NotEntityOwnerException ex)
            {
                return Forbid(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        // DELETE api/<JobsController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                if (appUser == null)
                {
                    return BadRequest("User not found.");
                }
                var deletedJob = await _repo.DeleteJob(id, appUser);
                return Ok(deletedJob);
            }
            catch(EntityNotFoundException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch (NotEntityOwnerException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
