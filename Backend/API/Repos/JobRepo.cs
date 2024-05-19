using API.Helpers.DTO;
using API.Helpers.Exceptions;
using API.Helpers.QueryModels;
using API.Interfaces.Repos;
using DataAccess;
using DataAccess.Auth;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace API.Repos
{
    public class JobRepo : IJobRepo
    {
        private ApiContext _context;
        private UserManager<AppUser> _userManager;

        public JobRepo(ApiContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<SmallJobDTO>> GetJobsAsync(JobQueryModal queryModal)
        {
            var jobs = _context.Jobs.Include(x => x.Employer).Include(x => x.Tags).AsQueryable();

            if (!queryModal.Title.IsNullOrEmpty())
            {
                jobs = jobs.Where(x => x.Title.Contains(queryModal.Title));
            }

            if(queryModal.EmployerId != null)
            {
                jobs = jobs.Where(x => x.Employer.Id == queryModal.EmployerId);
            }

            if(!queryModal.TagIds.IsNullOrEmpty())
            {
                List<int> tagIds = queryModal.TagIds.Split(',').Select(int.Parse).ToList();
                jobs = jobs.Where(x => x.Tags.Any(t => tagIds.Contains(t.Id)));
            }

            return await jobs.Skip(queryModal.PerPage * (queryModal.Page - 1)).Take(queryModal.PerPage).Select(x => new SmallJobDTO
            {
                Id = x.Id,
                Title = x.Title,
                Employer = new EmployerPartialDTO
                {
                    Name = x.Employer.Name,
                    Id = x.Employer.Id,
                },
                Tags = x.Tags.Select(t => new TagPartialDTO 
                { 
                    Id = t.Id,
                    Name = t.Name
                }).ToList(),
                
            }).ToListAsync();
        }

        public async Task<JobDTO> GetSingleJobAsync(int jobId)
        {
            var job = await _context.Jobs.Include(x => x.Tags).Include(x => x.Employer).FirstOrDefaultAsync(x => x.Id == jobId);

            if(job == null)
            {
                throw new EntityNotFoundException("Job does not exist");
            }

            return new JobDTO
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                Employer = new EmployerPartialDTO
                {
                    Name = job.Employer.Name,
                    Id = job.Employer.Id,
                },
                Tags = job.Tags.Select(t => new TagPartialDTO
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToList(),
            };
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
                Id = newJob.Id,
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

        public async Task<JobDTO> UpdateJob(int jobId, EditJobDTO editJobDTO, AppUser appUser)
        {
            var job = await _context.Jobs.Include(x => x.Tags).Include(x => x.Employer).FirstOrDefaultAsync(x => x.Id == jobId);

            if(job == null )
            {
                throw new EntityNotFoundException("This job does not exist.");
            }

            if(job.Employer.AppUserId != appUser.Id)
            {
                throw new NotEntityOwnerException("You are not the owner of this job post.");
            }

            if (!editJobDTO.Title.IsNullOrEmpty())
            {
                job.Title = editJobDTO.Title;
            }

            if(!editJobDTO.Description.IsNullOrEmpty())
            {
                job.Description = editJobDTO.Description;
            }

            if(!editJobDTO.TagIds.IsNullOrEmpty())
            {
                var newTags = await _context.Tags.Where(x => editJobDTO.TagIds.Contains(x.Id)).ToListAsync();
                job.Tags.Clear();
                job.Tags = newTags;
            }

            await _context.SaveChangesAsync();
            return new JobDTO
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                Employer = new EmployerPartialDTO
                {
                    Id = job.Employer.Id,
                    Name = job.Employer.Name,
                },
                Tags = job.Tags.Select(t => new TagPartialDTO
                {
                    Id = t.Id,
                    Name = t.Name,
                })
            };
        }

        public async Task<JobDTO> DeleteJob(int jobId, AppUser appUser)
        {
            var job = await _context.Jobs.Include(x => x.Tags).Include(x => x.Employer).FirstOrDefaultAsync(x => x.Id == jobId);

            if(job == null )
            {
                throw new EntityNotFoundException("Job does not exist.");
            }

            if (job.Employer.AppUserId != appUser.Id)
            {
                throw new NotEntityOwnerException("You are not the owner of this job post.");
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return new JobDTO
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                Employer = new EmployerPartialDTO
                {
                    Id = job.Employer.Id,
                    Name = job.Employer.Name,
                },
                Tags = job.Tags.Select(t => new TagPartialDTO
                {
                    Id = t.Id,
                    Name = t.Name,
                })
            };
        }
    }
}
