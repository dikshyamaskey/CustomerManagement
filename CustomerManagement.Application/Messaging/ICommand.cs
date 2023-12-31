using CustomerManagement.Core.Common.Model;
using MediatR;

namespace CustomerManagement.Application.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>> , IBaseCommand
{

}

public interface IBaseCommand
{
}