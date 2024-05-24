using API.Helpers.DTO;

namespace API.Interfaces.Repos
{
    public interface IRegularUserRepo
    {
        public Task<RegularUserSimpleDTO> CreateRegularUser(CreateRegularUserDTO createRegularUserDTO);
    }
}
