using DataAccess.Auth;

namespace API.Interfaces.Auth
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
