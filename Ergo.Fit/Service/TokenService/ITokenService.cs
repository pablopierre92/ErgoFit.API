using Ergo.Fit.Models;

namespace Ergo.Fit.Service.TokenService
{
    public interface ITokenService
    {
        string GenerateToken(ApplicationUser user, IList<string> roles);
    }
}
