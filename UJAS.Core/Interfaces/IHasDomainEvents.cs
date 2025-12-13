namespace UJAS.Core.Interfaces
{
    public interface IHasDomainEvents
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
        void ClearDomainEvents();
        void AddDomainEvent(IDomainEvent domainEvent);
    }
}