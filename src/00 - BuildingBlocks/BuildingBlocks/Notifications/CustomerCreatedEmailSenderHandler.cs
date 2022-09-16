using MediatR;

namespace BuildingBlocks.Notifications;

public class CustomerCreatedEmailSenderHandler : INotificationHandler<CustomerCreatedEvent>
{
    public Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
    {
        // IMessageService.Sent($"Welcome {notification.FirstName} {notification.LastName}, Nice to meet you .. !!!!");
        return Task.CompletedTask;
    }
}
