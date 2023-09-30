using CustomerManagement.Application.Interface;
using CustomerManagement.Application.Messaging;
using CustomerManagement.Application.Users.Commands.CreateUser;
using CustomerManagement.Core.Common.Model;
using CustomerManagement.Core.Entities;
using CustomerManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Application.Users.EventHandlers
{
    internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, string>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Membership> _memberShipRepository;
        public CreateUserCommandHandler(IRepository<User> userRepository, IUnitOfWork unitOfWork,IRepository<Customer> customerRepository,IRepository<Membership> memberShipRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _memberShipRepository = memberShipRepository;
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
            return "user Insert Successfully";
        }
    }
}
