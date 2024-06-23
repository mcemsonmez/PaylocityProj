using System;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.DataContext
{
	public class ApplicationContext : DbContext
	{
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
		{
			
		}

		public DbSet<Dependent> Dependents { get; set; }
		public DbSet<Employee> Employees { get; set; }

		public void Seed()
		{
            this.Database.EnsureCreated();
            if (!this.Employees.Any())
            {
                this.Employees.AddRange(new List<Employee>
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "LeBron",
                        LastName = "James",
                        Salary = 75420.99m,
                        DateOfBirth = new DateTime(1984, 12, 30)
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Ja",
                        LastName = "Morant",
                        Salary = 92365.22m,
                        DateOfBirth = new DateTime(1999, 8, 10),
                        Dependents = new List<Dependent>
                        {
                            new()
                            {
                                Id = 1,
                                FirstName = "Spouse",
                                LastName = "Morant",
                                Relationship = Relationship.Spouse,
                                DateOfBirth = new DateTime(1998, 3, 3),
                                EmployeeId = 2
                            },
                            new()
                            {
                                Id = 2,
                                FirstName = "Child1",
                                LastName = "Morant",
                                Relationship = Relationship.Child,
                                DateOfBirth = new DateTime(2020, 6, 23),
                                EmployeeId = 2
                            },
                            new()
                            {
                                Id = 3,
                                FirstName = "Child2",
                                LastName = "Morant",
                                Relationship = Relationship.Child,
                                DateOfBirth = new DateTime(2021, 5, 18),
                                EmployeeId = 2
                            }
                        }
                    },
                    new()
                    {
                        Id = 3,
                        FirstName = "Michael",
                        LastName = "Jordan",
                        Salary = 143211.12m,
                        DateOfBirth = new DateTime(1963, 2, 17),
                        Dependents = new List<Dependent>
                        {
                            new()
                            {
                                Id = 4,
                                FirstName = "DP",
                                LastName = "Jordan",
                                Relationship = Relationship.DomesticPartner,
                                DateOfBirth = new DateTime(1974, 1, 2),
                                EmployeeId = 3
                            }
                        }
                    }, // Over 80k Tests
                        // Employee 4 - An employee with one dependent who is under 50 years old.
                    new()
                    {
                        Id = 4,
                        FirstName = "Employee4",
                        LastName = "Test",
                        Salary = 85000.00m,
                        DateOfBirth = new DateTime(1970, 1, 1),
                        Dependents = new List<Dependent>
                        {
                            new()
                            {
                                Id = 5,
                                FirstName = "Dependent5",
                                LastName = "Test",
                                Relationship = Relationship.Spouse,
                                DateOfBirth = new DateTime(1980, 1, 1),
                                EmployeeId = 4
                            }
                        }
                    },
                    // Employee 5 - An employee with multiple dependents who are all under 50 years old.
                    new()
                    {
                        Id = 5,
                        FirstName = "Employee5",
                        LastName = "Test",
                        Salary = 90000.00m,
                        DateOfBirth = new DateTime(1980, 2, 2),
                        Dependents = new List<Dependent>
                        {
                            new()
                            {
                                Id = 6,
                                FirstName = "Dependent6",
                                LastName = "Test",
                                Relationship = Relationship.Child,
                                DateOfBirth = new DateTime(2000, 2, 2),
                                EmployeeId = 5
                            },
                            new()
                            {
                                Id = 7,
                                FirstName = "Dependent7",
                                LastName = "Test",
                                Relationship = Relationship.Child,
                                DateOfBirth = new DateTime(2005, 3, 3),
                                EmployeeId = 5
                            }
                        }
                    },
                    // Employee 6 - An employee with one dependent who is over 50 years old.
                    new()
                    {
                        Id = 6,
                        FirstName = "Employee6",
                        LastName = "Test",
                        Salary = 87000.00m,
                        DateOfBirth = new DateTime(1965, 3, 3),
                        Dependents = new List<Dependent>
                        {
                            new()
                            {
                                Id = 8,
                                FirstName = "Dependent8",
                                LastName = "Test",
                                Relationship = Relationship.Spouse,
                                DateOfBirth = new DateTime(1950, 4, 4),
                                EmployeeId = 6
                            }
                        }
                    },
                    // Employee 7 - An employee with multiple dependents, including one over 50 years old.
                    new()
                    {
                        Id = 7,
                        FirstName = "Employee7",
                        LastName = "Test",
                        Salary = 91000.00m,
                        DateOfBirth = new DateTime(1975, 4, 4),
                        Dependents = new List<Dependent>
                        {
                            new()
                            {
                                Id = 9,
                                FirstName = "Dependent9",
                                LastName = "Test",
                                Relationship = Relationship.Child,
                                DateOfBirth = new DateTime(2005, 5, 5),
                                EmployeeId = 7
                            },
                            new()
                            {
                                Id = 10,
                                FirstName = "Dependent10",
                                LastName = "Test",
                                Relationship = Relationship.DomesticPartner,
                                DateOfBirth = new DateTime(1960, 6, 6),
                                EmployeeId = 7
                            }
                        }
                    },
                    // Employee 8 - An employee with multiple dependents, including multiple dependents over 50 years old.
                    new()
                    {
                        Id = 8,
                        FirstName = "Employee8",
                        LastName = "Test",
                        Salary = 95000.00m,
                        DateOfBirth = new DateTime(1960, 5, 5),
                        Dependents = new List<Dependent>
                        {
                            new()
                            {
                                Id = 11,
                                FirstName = "Dependent11",
                                LastName = "Test",
                                Relationship = Relationship.Child,
                                DateOfBirth = new DateTime(1970, 7, 7),
                                EmployeeId = 8
                            },
                            new()
                            {
                                Id = 12,
                                FirstName = "Dependent12",
                                LastName = "Test",
                                Relationship = Relationship.Child,
                                DateOfBirth = new DateTime(1965, 8, 8),
                                EmployeeId = 8
                            }
                        }
                    },

                     // Under 80k Tests
                     // Employee 9 - An employee with one dependent who is under 50 years old.
                    new()
                    {
                        Id = 9,
                        FirstName = "Employee9",
                        LastName = "Test",
                        Salary = 65000.00m,
                        DateOfBirth = new DateTime(1970, 1, 1),
                        Dependents = new List<Dependent>
                        {
                            new()
                            {
                                Id = 13,
                                FirstName = "Dependent13",
                                LastName = "Test",
                                Relationship = Relationship.Spouse,
                                DateOfBirth = new DateTime(1980, 1, 1),
                                EmployeeId = 9
                            }
                        }
                    },
                     // Employee 10 - An employee with multiple dependents who are all under 50 years old.
                    new()
                    {
                        Id = 10,
                        FirstName = "Employee10",
                        LastName = "Test",
                        Salary = 70000.00m,
                        DateOfBirth = new DateTime(1980, 2, 2),
                        Dependents = new List<Dependent>
                        {
                            new()
                            {
                                Id = 14,
                                FirstName = "Dependent14",
                                LastName = "Test",
                                Relationship = Relationship.Child,
                                DateOfBirth = new DateTime(2000, 2, 2),
                                EmployeeId = 10
                            },
                            new()
                            {
                                Id = 15,
                                FirstName = "Dependent15",
                                LastName = "Test",
                                Relationship = Relationship.Child,
                                DateOfBirth = new DateTime(2005, 3, 3),
                                EmployeeId = 10
                            }
                        }
                    },
                    // Employee 11 - An employee with one dependent who is over 50 years old.
                    new()
                    {
                        Id = 11,
                        FirstName = "Employee11",
                        LastName = "Test",
                        Salary = 67000.00m,
                        DateOfBirth = new DateTime(1965, 3, 3),
                        Dependents = new List<Dependent>
                        {
                            new()
                            {
                                Id = 16,
                                FirstName = "Dependent16",
                                LastName = "Test",
                                Relationship = Relationship.Spouse,
                                DateOfBirth = new DateTime(1950, 4, 4),
                                EmployeeId = 11
                            }
                        }
                    },
                    // Employee 12 - An employee with multiple dependents, including one over 50 years old.
                    new()
                    {
                        Id = 12,
                        FirstName = "Employee12",
                        LastName = "Test",
                        Salary = 71000.00m,
                        DateOfBirth = new DateTime(1975, 4, 4),
                        Dependents = new List<Dependent>
                        {
                            new()
                            {
                                Id = 17,
                                FirstName = "Dependent17",
                                LastName = "Test",
                                Relationship = Relationship.Child,
                                DateOfBirth = new DateTime(2005, 5, 5),
                                EmployeeId = 12
                            },
                            new()
                            {
                                Id = 18,
                                FirstName = "Dependent18",
                                LastName = "Test",
                                Relationship = Relationship.DomesticPartner,
                                DateOfBirth = new DateTime(1960, 6, 6),
                                EmployeeId = 12
                            }
                        }
                    },
                    // Employee 13 - An employee with multiple dependents, including multiple dependents over 50 years old.
                    new()
                    {
                        Id = 13,
                        FirstName = "Employee13",
                        LastName = "Test",
                        Salary = 75000.00m,
                        DateOfBirth = new DateTime(1960, 5, 5),
                        Dependents = new List<Dependent>
                        {
                            new()
                            {
                                Id = 19,
                                FirstName = "Dependent19",
                                LastName = "Test",
                                Relationship = Relationship.Child,
                                DateOfBirth = new DateTime(1970, 7, 7),
                                EmployeeId = 13
                            },
                            new()
                            {
                                Id = 20,
                                FirstName = "Dependent20",
                                LastName = "Test",
                                Relationship = Relationship.Child,
                                DateOfBirth = new DateTime(1965, 8, 8),
                                EmployeeId = 13
                            }
                        } },

                    // Spouse - Domestic Partner Test Employee
                    new()
                    {
                         Id = 1000,
                         FirstName = "SpouseOrDomesticPartnerTest",
                         LastName = "Test",
                         DateOfBirth = new DateTime(1980, 1, 1),
                         Salary = 70000,
                         Dependents = new List<Dependent>
                         {
                             new()
                             {
                                  Id = 21,
                                  EmployeeId = 1000,
                                  FirstName = "Spouse1",
                                  LastName = "Test",
                                  Relationship = Relationship.Spouse,
                                  DateOfBirth = new DateTime(1984, 1, 1),
                                  
                             }
                         }
                    } }
                );
            }
            this.SaveChanges();
		}
     }
}

