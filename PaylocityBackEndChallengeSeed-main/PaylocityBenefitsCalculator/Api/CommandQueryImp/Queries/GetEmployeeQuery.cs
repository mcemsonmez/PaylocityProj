using System;
using Api.DataContext;
using Api.Dtos.Employee;
using Api.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.CommandQueryImp.Queries
{
    public class GetEmployeeQuery : IQuery<ApiResponse<GetEmployeeDto>>
    {
        public int Id { get; set; }
    }

    public class GetEmployeeQueryHandler : IQueryHandler<GetEmployeeQuery, ApiResponse<GetEmployeeDto>>
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        public GetEmployeeQueryHandler(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        async Task<ApiResponse<GetEmployeeDto>> IRequestHandler<GetEmployeeQuery, ApiResponse<GetEmployeeDto>>.Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var emp = _context.Employees.Include(e => e.Dependents).FirstOrDefault(predicate: e => e.Id == request.Id);
                if (emp == null)
                {
                    return new ApiResponse<GetEmployeeDto>
                    {
                        Data = null,
                        Error = "NotFound",
                        Success = false,
                    };
                }
                var dto = _mapper.Map<Employee, GetEmployeeDto>(emp);
                return new ApiResponse<GetEmployeeDto>
                {
                    Data = dto,
                    Error = string.Empty,
                    Success = true,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<GetEmployeeDto>
                {
                    Data = null,
                    Error = ex.Message,
                    Success = false,
                    Message = "Error Retrival of Employee."
                };
            }
        }
    }
}