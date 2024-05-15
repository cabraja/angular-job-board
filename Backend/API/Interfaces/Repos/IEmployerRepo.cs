using API.Helpers.DTO;
using API.Helpers.QueryModels;
using DataAccess.Models;

namespace API.Interfaces.Repos
{
    public interface IEmployerRepo
    {
        public Task<List<SmallEmployerDTO>> GetEmployersAsync(EmployerQueryModel queryModel);
        public Task<EmployerDTO> GetSingleEmployerAsync(int id);
    }
}
