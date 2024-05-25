using API.Helpers.DTO;
using API.Helpers.Exceptions;
using API.Helpers.QueryModels;
using API.Helpers.Validators;
using API.Interfaces.Repos;
using DataAccess;
using DataAccess.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Repos
{
    public class EmployerRepo : IEmployerRepo
    {
        private ApiContext _context;
        private EditEmployerDTOValidator _validator;

        public EmployerRepo(ApiContext context, EditEmployerDTOValidator validator)
        {
            _context = context;
            _validator = validator;
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

        public async Task<SmallEmployerDTO> CreateEmployerSimple(CreateEmployerDTO createDto)
        {
            if (createDto.EmployerName.IsNullOrEmpty())
            {
                throw new InputValidationException("Employer name was not provided.");
            }

            if (createDto.AppUserId.IsNullOrEmpty())
            {
                throw new InputValidationException("User ID was not provided.");
            }

            if(await _context.Employers.AnyAsync(x => x.Name.ToLower() == createDto.EmployerName.ToLower()))
            {
                throw new NameTakenException("An employer with this name already exists.");
            }

            Employer employer = new Employer
            {
                Name = createDto.EmployerName,
                AppUserId = createDto.AppUserId,
            };

            await _context.Employers.AddAsync(employer);
            await _context.SaveChangesAsync();

            return new EmployerDTO
            {
                Id = employer.Id,
                Name = employer.Name,
            };
        }

        public async Task<EmployerDTO> EditEmployer(EditEmployerDTO employerDTO, string appUserId, int employerId)
        {
            await _validator.ValidateAndThrowAsync(employerDTO);

            var employer = await _context.Employers.Include(x => x.Jobs).FirstOrDefaultAsync(x => x.Id == employerId);

            if(employer == null) 
            {
                throw new EntityNotFoundException("This employer does not exist.");
            }

            if (employer.AppUserId != appUserId)
            {
                throw new NotEntityOwnerException("You are not the owner of this job.");
            }

            Employer newEmployer = new Employer
            {
                Name = employerDTO.Name.IsNullOrEmpty() ? employer.Name : employerDTO.Name ,
                Address = employerDTO.Address.IsNullOrEmpty() ? employer.Address : employerDTO.Address,
                Description = employerDTO.Description.IsNullOrEmpty() ? employer.Description : employerDTO.Description,
                EmployeeCount = employerDTO.EmployeeCount.IsNullOrEmpty() ? employer.EmployeeCount : employerDTO.EmployeeCount,
                ImageUrl = employerDTO.ImageUrl.IsNullOrEmpty() ? employer.ImageUrl : employerDTO.ImageUrl
            };

            if (!employerDTO.Name.IsNullOrEmpty())
            {
                employer.Name = employerDTO.Name;
            }

            if (!employerDTO.Address.IsNullOrEmpty())
            {
                employer.Address = employerDTO.Address;
            }

            if(!employerDTO.Description.IsNullOrEmpty())
            {
                employer.Description = employerDTO.Description;
            }

            if(!employerDTO.EmployeeCount.IsNullOrEmpty()) 
            {
                employer.EmployeeCount = employerDTO.EmployeeCount;
            }

            if (!employerDTO.ImageUrl.IsNullOrEmpty())
            {
                employer.ImageUrl = employerDTO.ImageUrl;
            }

            await _context.SaveChangesAsync();

            return new EmployerDTO 
            {
                Id = employer.Id,
                Name = employer.Name,
                Address = employer.Address,
                Description = employer.Description,
                ImageUrl = employer.ImageUrl,
                EmployeeCount = employer.EmployeeCount,
                Jobs = employer.Jobs.Select(j => new PartialJobDTO { Id = j.Id, Title = j.Title, CreatedAt = j.CreatedAt }).ToList(),
            };
        }
    }
}
