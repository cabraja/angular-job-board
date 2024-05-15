using API.Helpers.Exceptions;
using API.Helpers.QueryModels;
using API.Interfaces.Repos;
using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployersController : ControllerBase
    {
        private IEmployerRepo _repo;

        public EmployersController(IEmployerRepo repo)
        {
            _repo = repo;
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

    }
}
