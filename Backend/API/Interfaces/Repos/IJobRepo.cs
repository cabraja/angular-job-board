using API.Helpers.DTO;
using DataAccess.Models;

namespace API.Interfaces.Repos
{
    public interface IJobRepo
    {
        // public Task<List<JobDTO>> GetJobsAsync();
        public Task<JobDTO> CreateJob(AddJobDTO addJobDTO);
    }
}
