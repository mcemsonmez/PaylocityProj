﻿using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class  Employee
{
    [Key]
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public virtual ICollection<Dependent>? Dependents { get; set; }
}
