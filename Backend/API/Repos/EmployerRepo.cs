using API.Helpers.DTO;
using API.Helpers.Exceptions;
using API.Helpers.QueryModels;
using API.Interfaces.Repos;
using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Repos
{
    public class EmployerRepo : IEmployerRepo
    {
        private ApiContext _context;

        public EmployerRepo(ApiContext context)
        {
            _context = context;
        }

        public async Task<List<SmallEmployerDTO>> GetEmployersAsync(EmployerQueryModel queryModel)
        {
            var employers = _context.Employers.AsQueryable();

            if (!queryModel.Name.IsNullOrEmpty())
            {
                employers = employers.Where(x => x.Name.Contains(queryModel.Name));
            }

            return await employers.Skip(queryModel.PerPage * (queryModel.Page-1)).Take(queryModel.PerPage).Select(x => new SmallEmployerDTO
            {
                Id = x.Id,
                Name = x.Name,
                EmployeeCount = x.EmployeeCount,
                ImageUrl = x.ImageUrl
            }).ToListAsync();
        }

        public async Task<EmployerDTO> GetSingleEmployerAsync(int employerId)
        {
            var employer = await _context.Employers.Include(x => x.Jobs).FirstOrDefaultAsync(x => x.Id == employerId);

            if(employer == null)
            {
                throw new EntityNotFoundException("Employer does not exist");
            }

            return new EmployerDTO 
            {
                Id = employerId,
                Name = employer.Name,
                EmployeeCount = employer.EmployeeCount,
                ImageUrl = employer.ImageUrl,
                Address = employer.Address,
                Description = employer.Description,
                Jobs = employer.Jobs.Select(j => new PartialJobDTO { Id = j.Id, Title = j.Title, CreatedAt = j.CreatedAt }).ToList(),
            };
        }
    }
}
