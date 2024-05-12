using API.Helpers;
using DataAccess.Models;

namespace API.Interfaces.Repos
{
    public interface IEmployerRepo
    {
        public Task<List<Employer>> GetEmployersAsync(EmployerQueryModel queryModel);
    }
}
