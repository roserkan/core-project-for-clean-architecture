using Shared.Security.Entities;

namespace Shared.Security.JWT;

public interface ITokenHelper
{
    AccessToken CreateToken(User user, IList<Role> roles);

    RefreshToken CreateRefreshToken(User user, string ipAddress);
}
