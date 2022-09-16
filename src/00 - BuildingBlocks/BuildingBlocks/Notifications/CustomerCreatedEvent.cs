﻿using MediatR;

namespace BuildingBlocks.Notifications;

public class CustomerCreatedEvent : INotification
{
    public CustomerCreatedEvent(string firstName, string lastName, DateTimeOffset registrationDate)
    {
        FirstName = firstName;
        LastName = lastName;
        RegistrationDate = registrationDate;
    }

    public string FirstName { get; }

    public string LastName { get; }

    public DateTimeOffset RegistrationDate { get; }
}
