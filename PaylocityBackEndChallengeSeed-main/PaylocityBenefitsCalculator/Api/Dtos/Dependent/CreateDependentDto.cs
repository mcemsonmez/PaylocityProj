using System;
using Api.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Dtos.Dependent
{
	public class CreateDependentDto
	{
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Relationship Relationship { get; set; }
        public int EmployeeId { get; set; }
    }
}

