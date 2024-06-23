using System;
using System.Runtime.Intrinsics.Arm;
using Api.DataContext;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.CommandQueryImp.Queries
{
	public class GetAllEmployeesQuery: IQuery<ApiResponse<IEnumerable<GetEmployeeDto>>>
	{
	}

    public class GetAllEmployeesQueryHandler : IQueryHandler<GetAllEmployeesQuery, ApiResponse<IEnumerable<GetEmployeeDto>>>
    {
        ApplicationContext _context;
        IMapper _mapper;
        public GetAllEmployeesQueryHandler(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ApiResponse<IEnumerable<GetEmployeeDto>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var emps = await _context.Employees.Include(e => e.Dependents).ToListAsync();
                if (emps == null)
                {
                    return new ApiResponse<IEnumerable<GetEmployeeDto>>
                    {
                        Data = null,
                        Success = false,
                    };
                }
                var mappedEmps = _mapper.Map<IEnumerable<Employee>, IEnumerable<GetEmployeeDto>>(emps);
                return new ApiResponse<IEnumerable<GetEmployeeDto>>
                {
                    Data = mappedEmps,
                    Error = string.Empty,
                    Message = string.Empty,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<GetEmployeeDto>>
                {
                    Data = null,
                    Error = "Error Retrival of Employees",
                    Message = ex.Message,
                    Success = false
                };
            }
           
        }
    }
}

