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

        public async Task<List<Employer>> GetEmployersAsync(EmployerQueryModel queryModel)
        {
            var employers = _context.Employers.Include(x => x.Jobs).AsQueryable();

            if (!queryModel.Name.IsNullOrEmpty())
            {
                employers = employers.Where(x => x.Name.Contains(queryModel.Name));
            }

            return await employers.Skip(queryModel.PerPage * (queryModel.Page-1)).Take(queryModel.PerPage).ToListAsync();
        }
    }
}
