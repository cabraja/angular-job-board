using API.Helpers.DTO;
using API.Helpers.Exceptions;
using API.Interfaces.Repos;
using DataAccess;
using DataAccess.Models;

namespace API.Repos
{
    public class JobRepo : IJobRepo
    {
        private ApiContext _context;

        public JobRepo(ApiContext context)
        {
            _context = context;
        }

        public async Task<JobDTO> CreateJob(AddJobDTO addJobDTO)
        {
            var employer = await _context.Employers.FindAsync(addJobDTO.EmployerId);

            if (employer == null)
            {
                throw new EntityNotFoundException("Employer does not exist.");
            }

            var tags = _context.Tags.Where(x => addJobDTO.TagIds.Contains(x.Id));

            if(!tags.Any() )
            {
                throw new EntityNotFoundException("Tag IDs are invalid.");
            }

            var newJob = new Job
            {
                Title = addJobDTO.Title,
                Description = addJobDTO.Description,
                Employer = employer,
                Tags = tags.ToList()
            };

            await _context.Jobs.AddAsync(newJob);
            await _context.SaveChangesAsync();

            return new JobDTO
            {
                Title = newJob.Title,
                Description = newJob.Description,
                Employer = new EmployerPartialDTO
                {
                    Id = employer.Id,
                    Name = employer.Name
                },
                Tags = tags.Select(x => new TagPartialDTO { Id = x.Id, Name = x.Name }).ToList()
            };
        }
    }
}
