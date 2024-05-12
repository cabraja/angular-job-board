using API.Helpers;
using API.Interfaces.Repos;
using DataAccess;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

            var employer = await _repo.GetEmployersAsync(queryModel);

            return Ok(employer);
        }

        
    }
}
