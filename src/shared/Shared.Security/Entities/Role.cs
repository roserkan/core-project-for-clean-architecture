using Shared.Persistence.Base;

namespace Shared.Security.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; }

    public Role() { }

    public Role(Guid id, string name)
        : base(id)
    {
        Name = name;
    }
}