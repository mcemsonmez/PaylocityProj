using System;
using MediatR;

namespace Api.CommandQueryImp
{
	public interface IQuery<TResult> : IRequest<TResult>
	{
	}
}

