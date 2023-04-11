using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Security.Encryption;
using Shared.Security.Entities;
using Shared.Security.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;


namespace Shared.Security.JWT;

public class JwtHelper : ITokenHelper
{
    public IConfiguration Configuration { get; }
    private readonly TokenOptions _tokenOptions;
    private DateTime _accessTokenExpiration;

    public JwtHelper(IConfiguration configuration)
    {
        Configuration = configuration;
        _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
    }

    public AccessToken CreateToken(User user, IList<Role> roles)
    {
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        JwtSecurityToken jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, roles);
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        string? token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new AccessToken { Token = token, Expiration = _accessTokenExpiration };
    }

    public RefreshToken CreateRefreshToken(User user, string ipAddress)
    {
        RefreshToken refreshToken =
            new()
            {
                UserId = user.Id,
                Token = RandomRefreshToken(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };

        return refreshToken;
    }

    public JwtSecurityToken CreateJwtSecurityToken(
        TokenOptions tokenOptions,
        User user,
        SigningCredentials signingCredentials,
        IList<Role> roles
    )
    {
        JwtSecurityToken jwt =
            new(
                tokenOptions.Issuer,
                tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, roles),
                signingCredentials: signingCredentials
            );
        return jwt;
    }

    private IEnumerable<Claim> SetClaims(User user, IList<Role> roles)
    {
        List<Claim> claims = new();
        claims.AddNameIdentifier(user.Id.ToString());
        claims.AddEmail(user.Email);
        claims.AddRoles(roles.Select(c => c.Name).ToArray());
        return claims;
    }

    private string RandomRefreshToken()
    {
        byte[] numberByte = new byte[32];
        using RandomNumberGenerator random = RandomNumberGenerator.Create();
        random.GetBytes(numberByte);
        return Convert.ToBase64String(numberByte);
    }
}