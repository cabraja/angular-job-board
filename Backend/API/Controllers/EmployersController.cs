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

        
    }
}
