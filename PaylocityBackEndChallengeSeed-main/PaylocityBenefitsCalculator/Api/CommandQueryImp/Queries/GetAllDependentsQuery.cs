using System;
using Api.DataContext;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.CommandQueryImp.Queries
{
	public class GetAllDependentsQuery: IQuery<ApiResponse<IEnumerable<GetDependentDto>>>
	{
	}

    public class GetAllDependentsQueryHandler : IQueryHandler<GetAllDependentsQuery, ApiResponse<IEnumerable<GetDependentDto>>>
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public GetAllDependentsQueryHandler(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<GetDependentDto>>> Handle(GetAllDependentsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var deps = await _context.Dependents.ToListAsync();
                if (deps == null)
                {
                    return new ApiResponse<IEnumerable<GetDependentDto>>
                    {
                        Data = null,
                        Error = "Dependents Not Found.",
                        Success = false,
                        Message = "There is no dependent."
                    };
                }
                var mappedEmps = _mapper.Map<IEnumerable<Dependent>, IEnumerable<GetDependentDto>>(deps);
                return new ApiResponse<IEnumerable<GetDependentDto>>
                {
                    Data = mappedEmps,
                    Error = string.Empty,
                    Message = string.Empty,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<GetDependentDto>>
                {
                    Data = null,
                    Error = "Error Retrival of Dependents",
                    Message = ex.Message,
                    Success = false
                };
            }
        }
    }
}

