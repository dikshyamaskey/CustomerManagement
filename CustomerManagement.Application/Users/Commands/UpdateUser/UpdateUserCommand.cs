using CustomerManagement.Application.Interface;
using CustomerManagement.Application.Messaging;
using CustomerManagement.Application.Users.Commands.CreateUser;
using CustomerManagement.Application.Users.Commands.CreateUser.Dto;
using CustomerManagement.Application.Users.Commands.UpdateUser.Dto;
using CustomerManagement.Core.Common.Model;
using CustomerManagement.Core.Entities;
using CustomerManagement.Core.Events.CommonEvents;
using CustomerManagement.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : UserUpdateRequestDto, ICommand<string>
    {
    }
    internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, string>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitofWorkFactory _unitOfWorkFactory;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Membership> _memberShipRepository;
        private readonly IMediator _mediator;
        public UpdateUserCommandHandler(IRepository<User> userRepository, IUnitofWorkFactory unitOfWorkFactory, IRepository<Customer> customerRepository, IRepository<Membership> memberShipRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _customerRepository = customerRepository;
            _memberShipRepository = memberShipRepository;
            _mediator = mediator;
        }

        public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {



            using (var uow = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var customer = await _customerRepository.Table().Where(x => x.Id == request.Id).FirstOrDefaultAsync();
                if (customer != null) {
                    customer.IsActive=true;
                }
                _customerRepository.Update(customer);
                await uow.CommitAsync(cancellationToken);
            }
         
            return "user Updated Successfully";
        }
    }
}
