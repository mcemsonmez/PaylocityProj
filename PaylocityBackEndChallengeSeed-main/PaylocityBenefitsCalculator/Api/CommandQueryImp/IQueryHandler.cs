using System;
using MediatR;

namespace Api.CommandQueryImp
{
	public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery: IQuery<TResult>
	{
	}
}

