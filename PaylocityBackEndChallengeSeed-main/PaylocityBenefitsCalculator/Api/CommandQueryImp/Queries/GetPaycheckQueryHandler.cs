using System;
using Api.CommandQueryImp.Queries;
using Api.DataContext;
using Api.Dtos.Paycheck;
using Api.Models;
using MediatR;

namespace Api.CommandQueryImp.Commands
{
	public class CalculatePaycheckQuery : IQuery<ApiResponse<PaycheckResultDto>>
	{
		public int EmployeeId { get; set; }
		public int PaycheckNumber{ get; set; }
	}

    public class GetPaycheckQueryHandler : IQueryHandler<CalculatePaycheckQuery, ApiResponse<PaycheckResultDto>>
    {
        ApplicationContext _context;
        IMediator _mediator;

        public GetPaycheckQueryHandler(ApplicationContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        async Task<ApiResponse<PaycheckResultDto>> IRequestHandler<CalculatePaycheckQuery, ApiResponse<PaycheckResultDto>>.Handle(CalculatePaycheckQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var emp = await _mediator.Send(new GetEmployeeQuery { Id = request.EmployeeId });
                if (!emp.Success)
                    return new ApiResponse<PaycheckResultDto>
                    {
                        Data = null,
                        Error = emp.Error,
                        Message = emp.Message,
                        Success = false
                    };
                decimal totalCost = AppContants.BaseCostPerMonth * 12m;
                // Dependents
                emp.Data.Dependents?.ToList().ForEach(d =>
                {
                    totalCost += (d.DateOfBirth > DateTime.Now.AddYears(-50) ?
                        AppContants.DependentCostPerMonth + AppContants.AdditionalCostOver50 :
                        AppContants.DependentCostPerMonth) * 12m;
                });
                // High Income Cost
                if (emp.Data.Salary > AppContants.HighIncomeThreshold)
                    totalCost += emp.Data.Salary * AppContants.AdditionalCostHighIncome;
                decimal termCost = totalCost / AppContants.PaychecksPerYear;
                decimal termSalary = emp.Data.Salary / AppContants.PaychecksPerYear;
                var payRes = new PaycheckResultDto {
                    Amount = termSalary - termCost,
                    EmployeeId = request.EmployeeId,
                    PaycheckNumber = request.PaycheckNumber };
                return new ApiResponse<PaycheckResultDto>
                {
                    Data = payRes,
                    Success = true,
                    Message = string.Empty,
                    Error = string.Empty
                };

            }
            catch (Exception ex)
            {
                return new ApiResponse<PaycheckResultDto>
                {
                    Data = null,
                    Success = false,
                    Message = ex.Message,
                    Error = "Paycheck Calculation Error"

                };
            }
        }
    }
}

