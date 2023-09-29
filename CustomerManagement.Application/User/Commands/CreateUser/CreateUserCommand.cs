using CustomerManagement.Application.Messaging;
using CustomerManagement.Application.User.Commands.CreateUser.Dto;
using CustomerManagement.Core.Common.Model;

namespace CustomerManagement.Application.User.Commands.CreateUser;

public class CreateUserCommand : UserCreateRequestDto, ICommand<string>
{
}

internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, string>
{
    
    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return "hello from other side";
    }
}