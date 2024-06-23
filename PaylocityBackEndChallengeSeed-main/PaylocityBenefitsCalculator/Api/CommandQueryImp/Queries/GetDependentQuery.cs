using Api.DataContext;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using AutoMapper;

namespace Api.CommandQueryImp.Queries
{
    public class GetDependentQuery: IQuery<ApiResponse<GetDependentDto>>
	{
		public int Id { get; set; }
	}

    public class GetDependentQueryHandler: IQueryHandler<GetDependentQuery, ApiResponse<GetDependentDto>>
    {
        ApplicationContext _context;
        IMapper _mapper;

        public GetDependentQueryHandler(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<GetDependentDto>> Handle(GetDependentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var dep = await _context.Dependents.FindAsync(request.Id);
                if (dep == null)
                {
                    return new ApiResponse<GetDependentDto>
                    {
                        Success = false,
                        Data = null,
                        Error = "NotFound"
                    };
                }
                var dto = _mapper.Map<Dependent, GetDependentDto>(dep);
                return new ApiResponse<GetDependentDto>
                {
                    Data = dto,
                    Error = string.Empty,
                    Success = true,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<GetDependentDto>
                {
                    Data = null,
                    Error = ex.Message,
                    Success = false,
                    Message = "Error Retrival of Dependent."
                };
            }
            

        }
    }
}

