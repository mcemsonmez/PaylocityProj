using Api.CommandQueryImp.Queries;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;
    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        var res = await _mediator.Send(new GetEmployeeQuery { Id = id });
        if (res.Error == "NotFound")
            return NotFound();
        return Ok(res);
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        //task: use a more realistic production approach
        #region Provided Data
        //var employees = new List<GetEmployeeDto>
        //{
        //    new()
        //    {
        //        Id = 1,
        //        FirstName = "LeBron",
        //        LastName = "James",
        //        Salary = 75420.99m,
        //        DateOfBirth = new DateTime(1984, 12, 30)
        //    },
        //    new()
        //    {
        //        Id = 2,
        //        FirstName = "Ja",
        //        LastName = "Morant",
        //        Salary = 92365.22m,
        //        DateOfBirth = new DateTime(1999, 8, 10),
        //        Dependents = new List<GetDependentDto>
        //        {
        //            new()
        //            {
        //                Id = 1,
        //                FirstName = "Spouse",
        //                LastName = "Morant",
        //                Relationship = Relationship.Spouse,
        //                DateOfBirth = new DateTime(1998, 3, 3)
        //            },
        //            new()
        //            {
        //                Id = 2,
        //                FirstName = "Child1",
        //                LastName = "Morant",
        //                Relationship = Relationship.Child,
        //                DateOfBirth = new DateTime(2020, 6, 23)
        //            },
        //            new()
        //            {
        //                Id = 3,
        //                FirstName = "Child2",
        //                LastName = "Morant",
        //                Relationship = Relationship.Child,
        //                DateOfBirth = new DateTime(2021, 5, 18)
        //            }
        //        }
        //    },
        //    new()
        //    {
        //        Id = 3,
        //        FirstName = "Michael",
        //        LastName = "Jordan",
        //        Salary = 143211.12m,
        //        DateOfBirth = new DateTime(1963, 2, 17),
        //        Dependents = new List<GetDependentDto>
        //        {
        //            new()
        //            {
        //                Id = 4,
        //                FirstName = "DP",
        //                LastName = "Jordan",
        //                Relationship = Relationship.DomesticPartner,
        //                DateOfBirth = new DateTime(1974, 1, 2)
        //            }
        //        }
        //    }
        //};
        #endregion
        var employees =await _mediator.Send(new GetAllEmployeesQuery());
        return Ok(employees);
    }
}
