using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Api.Dtos.Employee;
using Api.Models;

namespace Api.Dtos.Dependent;

public class GetDependentDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    
    public Relationship Relationship { get; set; }
    [JsonIgnore]
    public int EmployeeId { get; set; }
    [JsonIgnore]
    public GetEmployeeDto Employee { get; set; }
}
