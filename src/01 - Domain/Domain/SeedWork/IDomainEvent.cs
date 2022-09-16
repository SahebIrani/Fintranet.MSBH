using System;
//using MediatR;

namespace Domain.SeedWork;

public interface IDomainEvent //: INotification
{
    DateTime OccurredOn { get; }
}