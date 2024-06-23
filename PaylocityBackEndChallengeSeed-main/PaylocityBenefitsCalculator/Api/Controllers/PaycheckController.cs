using System;
using Api.CommandQueryImp.Commands;
using Api.Dtos.Paycheck;
using Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaycheckController : ControllerBase
    {
        IMediator _mediator;

        public PaycheckController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("")]
        public async Task<ApiResponse<PaycheckResultDto>> CalculatePaycheck(CalculatePaycheckDto calculatePaycheckDto)
		{
            return await _mediator.Send(new CalculatePaycheckQuery
            {
                EmployeeId = calculatePaycheckDto.EmployeeId,
                PaycheckNumber = calculatePaycheckDto.PaycheckNumber
            });
		}
	}
}

