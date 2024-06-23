using System.Text.Json.Serialization;
using Api.Dtos.Dependent;

namespace Api.Dtos.Employee;

public class GetEmployeeDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public IEnumerable<GetDependentDto>? Dependents { get; set; }
    [JsonIgnore]
    public bool HasSpouseOrDomesticPartner => this.Dependents?.Any(x => x.Relationship == Models.Relationship.Spouse || x.Relationship == Models.Relationship.DomesticPartner) ?? false;
}
