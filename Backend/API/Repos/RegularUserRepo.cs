using API.Helpers.DTO;
using API.Helpers.Exceptions;
using API.Interfaces.Repos;
using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Repos
{
    public class RegularUserRepo : IRegularUserRepo
    {
        private readonly ApiContext _context;
        public RegularUserRepo(ApiContext context) 
        {
            _context = context;
        }
        public async Task<RegularUserSimpleDTO> CreateRegularUser(CreateRegularUserDTO createRegularUserDTO)
        {
            if (createRegularUserDTO.Username.IsNullOrEmpty())
            {
                throw new InputValidationException("Username was not provided.");
            }

            if (createRegularUserDTO.AppUserId.IsNullOrEmpty())
            {
                throw new InputValidationException("User ID was not provided.");
            }

            if(await _context.RegularUsers.AnyAsync(x => x.Username.ToLower() == createRegularUserDTO.Username.ToLower()))
            {
                throw new NameTakenException("This username is already taken.");
            }

            RegularUser newUser = new RegularUser 
            {
                Username = createRegularUserDTO.Username,
                AppUserId = createRegularUserDTO.AppUserId
            };

            await _context.RegularUsers.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return new RegularUserSimpleDTO 
            { 
                Username = newUser.Username,
            };
        }
    }
}
