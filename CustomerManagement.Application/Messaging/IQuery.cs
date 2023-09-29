using CustomerManagement.Application.Common.Model;
using CustomerManagement.Core.Common.Model;
using MediatR;

namespace CustomerManagement.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}

public interface IQueryPaginated<TResponse> : IRequest<Result<PaginatedList<TResponse>>>
{

}