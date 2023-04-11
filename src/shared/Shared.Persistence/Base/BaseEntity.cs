using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Persistence.Base;

public abstract class BaseEntity<TId>
{
    public TId Id { get; set; }

    private readonly List<BaseEvent> _domainEvents = new();

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();


    public BaseEntity(TId id)
    {
        Id = id;
    }

    public BaseEntity()
    {
    }

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
