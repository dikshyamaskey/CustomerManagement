using CustomerManagement.Application.Messaging;
using CustomerManagement.Application.Users.Queries.FetchCustomerQuery.Dto;
using CustomerManagement.Core.Common.Model;
using CustomerManagement.Core.Entities;
using CustomerManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Application.Users.Queries.FetchCustomerQuery;

public record FetchCustomerQuery(Guid id) : IQuery<CustomerDetailDto>;

internal sealed class FetchCustomerQueryHandler : IQueryHandler<FetchCustomerQuery, CustomerDetailDto>
{
    private readonly IRepository<Customer> _customerRepository;
    public FetchCustomerQueryHandler(IRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }
    public async Task<Result<CustomerDetailDto>> Handle(FetchCustomerQuery request, CancellationToken cancellationToken)
    {
        return await _customerRepository.TableNoTracking().Where(x => x.Id == request.id).Select(x => new CustomerDetailDto()
        {
            FirstName = x.User.FirstName,
            LastName = x.User.LastName
        }).FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}