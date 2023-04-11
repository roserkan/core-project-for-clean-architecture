using Shared.Persistence.Base;
using Shared.Security.Enums;

namespace Shared.Security.Entities;

public class User : BaseAuditableEntity<Guid>
{
    public string Email { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
    public bool Status { get; set; }
    public AuthenticatorType AuthenticatorType { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; }
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; }

    public User()
    {
    }

    public User(
        Guid id,
        string firstName,
        string lastName,
        string email,
        byte[] passwordSalt,
        byte[] passwordHash,
        bool status,
        AuthenticatorType authenticatorType
    )
        : base(id)
    {
        Id = id;
        Email = email;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
        Status = status;
        AuthenticatorType = authenticatorType;
        UserRoles = new HashSet<UserRole>();
        RefreshTokens = new HashSet<RefreshToken>();
    }
}
