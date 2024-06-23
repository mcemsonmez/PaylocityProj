using System;
using Api.CommandQueryImp.Queries;
using Api.DataContext;
using Api.Dtos.Dependent;
using Api.Models;
using AutoMapper;
using MediatR;

namespace Api.CommandQueryImp.Commands
{
	public class CreateDependentCommand : GetDependentDto, ICommand
	{
		public int EmployeeId { get; set; }
	}

    public class CreateDependentCommandHandler : ICommandHandler<CreateDependentCommand>
    {
        ApplicationContext _context;
        IMediator _mediator;
        IMapper _mapper;
        public CreateDependentCommandHandler(ApplicationContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task Handle(CreateDependentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var emp = await _mediator.Send(new GetEmployeeQuery { Id = request.EmployeeId });
                if (emp.Success)
                {
                    if (request.Relationship == Models.Relationship.Spouse || request.Relationship == Models.Relationship.DomesticPartner
                        && emp.Data.HasSpouseOrDomesticPartner)
                        throw new Exception("Only one spouse or domestic partner could be add.");
                }
                var dep = _mapper.Map<CreateDependentCommand, Dependent>(request);
                _context.Dependents.Add(dep);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
    }
}

