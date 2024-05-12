using API.Helpers.DTO;
using API.Helpers.Exceptions;
using API.Interfaces.Repos;
using API.Repos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private IJobRepo _repo;

        public JobsController(IJobRepo repo)
        {
            _repo = repo;
        }

        // GET: api/<JobsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<JobsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<JobsController>
        [HttpPost]
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

        // PUT api/<JobsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<JobsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
