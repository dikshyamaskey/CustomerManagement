using CustomerManagement.Application.Users.Commands.CreateUser;
using CustomerManagement.Core.Entities;
using CustomerManagement.Core.Events.CommonEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Application.Users.EventHandlers;

public class CreateUserEventHandler : INotificationHandler<CompleteEvent<Core.Entities.User>>
{
    private readonly ILogger<CompleteEvent<Core.Entities.User>> _logger;

    public CreateUserEventHandler(ILogger<CompleteEvent<Core.Entities.User>> logger)
    {
        _logger = logger;
    }
    public Task Handle(CompleteEvent<User> notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("New user inserted", notification.obj.UserName);
        return Task.CompletedTask;    }
}