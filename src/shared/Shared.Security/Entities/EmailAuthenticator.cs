using Shared.Persistence.Base;

namespace Shared.Security.Entities;

public class EmailAuthenticator : BaseAuditableEntity<Guid>
{
    public Guid UserId { get; set; }
    public string? ActivationKey { get; set; }
    public bool IsVerified { get; set; }

    public virtual User User { get; set; }

    public EmailAuthenticator() { }

    public EmailAuthenticator(Guid id, Guid userId, string? activationKey, bool isVerified)
        : base(id)
    {
        Id = id;
        UserId = userId;
        ActivationKey = activationKey;
        IsVerified = isVerified;
    }
}
