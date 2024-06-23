using System;
using MediatR;

namespace Api.CommandQueryImp
{
	public interface ICommandHandler<TCommand>: IRequestHandler<TCommand> where TCommand: ICommand 
	{
	}
}

