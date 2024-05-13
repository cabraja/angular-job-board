using API.Helpers.DTO;
using API.Helpers.QueryModels;
using DataAccess.Models;

namespace API.Interfaces.Repos
{
    public interface IJobRepo
    {
        public Task<List<JobDTO>> GetJobsAsync(JobQueryModal queryModal);
        public Task<JobDTO> CreateJob(AddJobDTO addJobDTO);
    }
}
