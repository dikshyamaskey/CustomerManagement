using CustomerManagement.Application.Interface;
using CustomerManagement.Application.Messaging;
using CustomerManagement.Application.Users.Commands.CreateUser.Dto;
using CustomerManagement.Core.Common.Model;
using CustomerManagement.Core.Entities;
using CustomerManagement.Core.Events.CommonEvents;
using CustomerManagement.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Application.Users.Commands.CreateUser;

public class CreateUserCommand : UserCreateRequestDto, ICommand<string>
{
}

internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, string>
{
    private readonly IRepository<User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Customer> _customerRepository;
    private readonly IRepository<Membership> _memberShipRepository;
    private readonly IMediator _mediator;
    public CreateUserCommandHandler(IRepository<User> userRepository, IUnitOfWork unitOfWork,IRepository<Customer> customerRepository,IRepository<Membership> memberShipRepository, IMediator mediator)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _customerRepository = customerRepository;
        _memberShipRepository = memberShipRepository;
        _mediator = mediator;
    }

    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Address = request.Address,
            EmailAddress = request.EmailAddress,
            Password = request.Password,
            UserName = request.UserName
        };

       await _userRepository.InsertAsync(user,cancellationToken);
       var memberShipId = (await _memberShipRepository.Table().FirstOrDefaultAsync(cancellationToken: cancellationToken)).Id;
       await _customerRepository.InsertAsync(new Customer()
       {
           UserId = user.Id,
           MembershipId = memberShipId,
           IsActive = true
       }, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        await _mediator.Publish(new CompleteEvent<User>(user) , cancellationToken : cancellationToken);
        return "user Insert Successfully";
    }
}
