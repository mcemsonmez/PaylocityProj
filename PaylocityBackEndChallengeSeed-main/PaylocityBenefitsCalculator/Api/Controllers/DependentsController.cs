using Api.CommandQueryImp.Commands;
using Api.CommandQueryImp.Queries;
using Api.Dtos.Dependent;
using Api.Models;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    IMediator _mediatr;
    IMapper _mapper;
    public DependentsController(IMediator mediatr, IMapper mapper)
    {
        _mediatr = mediatr;
        _mapper = mapper;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        var res = await _mediatr.Send(new GetDependentQuery { Id = id });
        if (res.Error == "NotFound")
            return NotFound();
        return Ok(res);
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        return Ok(await _mediatr.Send(new GetAllDependentsQuery()));
    }

    [SwaggerOperation(Summary = "Create dependent")]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> CreateDependent(CreateDependentDto createDependentDto)
    {
        try
        {
            var command = _mapper.Map<CreateDependentDto, CreateDependentCommand>(createDependentDto);
            Task task = _mediatr.Send(command);
            if (task.IsCompleted)
            {
                var mappedDep = _mapper.Map<GetDependentDto, CreateDependentCommand>(command);
                var res = new ApiResponse<GetDependentDto>
                {
                    Data = mappedDep,
                    Success = true,
                    Message = string.Empty,
                    Error = string.Empty
                };
                return Ok(res);
            }
            else
            {
                var res = new ApiResponse<GetDependentDto>
                {
                    Data = null,
                    Success = false,
                    Message = "Dependent Add Error",
                    Error = task.Exception?.Message
                };
                return Ok(res);
            }
        }   
        catch (Exception ex)
        {
            return Ok(new ApiResponse<GetDependentDto>
            {
                Data = null,
                Success = false,
                Message = "Dependent Add Error",
                Error = ex.Message
            });
        }
        
    }
}
