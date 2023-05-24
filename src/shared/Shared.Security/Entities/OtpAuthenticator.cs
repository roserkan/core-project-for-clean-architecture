using Shared.Persistence.Base;

namespace Shared.Security.Entities;

public class OtpAuthenticator : BaseEntity
{
    public Guid UserId { get; set; }
    public byte[] SecretKey { get; set; }
    public bool IsVerified { get; set; }

    public virtual User User { get; set; }

    public OtpAuthenticator() { }

    public OtpAuthenticator(Guid id, Guid userId, byte[] secretKey, bool isVerified)
        : base(id)
    {
        Id = id;
        UserId = userId;
        SecretKey = secretKey;
        IsVerified = isVerified;
    }
}