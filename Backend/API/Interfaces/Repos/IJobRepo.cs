using API.Helpers.DTO;
using API.Helpers.QueryModels;
using DataAccess.Auth;
using DataAccess.Models;

namespace API.Interfaces.Repos
{
    public interface IJobRepo
    {
        public Task<List<SmallJobDTO>> GetJobsAsync(JobQueryModal queryModal);
        public Task<JobDTO> GetSingleJobAsync(int jobId);
        public Task<JobDTO> CreateJob(AddJobDTO addJobDTO);
        public Task<JobDTO> UpdateJob(int JobId, EditJobDTO editJobDTO, AppUser appUser);
        public Task<JobDTO> DeleteJob(int JobId, AppUser appUser);
        public Task<List<SmallJobDTO>> GetFavoriteJobsAsync(PaginatedQueryModal queryModal, AppUser appUser);
    }
}
