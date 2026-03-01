using MediatR;

namespace SubSnap.Core.Domain.Common;

/*
 * qui colleghi DomainEvent --> plugin MediatR, in questo modo ogni volta che viene creato un DomainEvent, questo viene automaticamente pubblicato (pub/sub) da MediatR, e tutti i DomainEventHandler che lo gestiscono vengono eseguiti.
 */
public interface IDomainEvent : INotification
{
    DateTime OccurredOnUtc { get; }
}
