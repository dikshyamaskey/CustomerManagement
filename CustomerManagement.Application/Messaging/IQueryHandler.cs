using CustomerManagement.Application.Common.Model;
using CustomerManagement.Core.Common.Model;
using MediatR;

namespace CustomerManagement.Application.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}

public interface IQueryHandlerPaginated<TQuery, TResponse> : IRequestHandler<TQuery, Result<PaginatedList<TResponse>>>
    where TQuery : IQueryPaginated<TResponse>
{
}