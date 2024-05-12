using API.Helpers;
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
            var employers = _context.Employers.AsQueryable();

            if (!queryModel.Name.IsNullOrEmpty())
            {
                employers = employers.Where(x => x.Name.Contains(queryModel.Name));
            }

            return await employers.ToListAsync();
        }
    }
}
