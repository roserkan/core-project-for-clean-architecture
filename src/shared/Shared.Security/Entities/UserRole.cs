using Shared.Persistence.Base;

namespace Shared.Security.Entities;

public class UserRole : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }

    public virtual User User { get; set; }
    public virtual Role Role { get; set; }

    public UserRole() { }

    public UserRole(Guid id, Guid userId, Guid roleId)
        : base(id)
    {
        UserId = userId;
        RoleId = roleId;
    }
}