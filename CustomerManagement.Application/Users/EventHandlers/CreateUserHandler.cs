using CustomerManagement.Application.Messaging;
using CustomerManagement.Application.Users.Commands.CreateUser;
using CustomerManagement.Core.Common.Model;
using CustomerManagement.Core.Entities;
using CustomerManagement.Infrastructure.Data;

namespace CustomerManagement.Application.Users.EventHandlers
{
    public class CreateUserHandler : ICommandHandler<CreateUserCommand, string>
    {
        private readonly IRepository<User> _userRepository;

        public CreateUserHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                EmailAddress = request.EmailAddress
            };

            _userRepository.Insert(user);
            await _userRepository.SaveChangesAsync(cancellationToken);
            return "";
        }
    }
}
