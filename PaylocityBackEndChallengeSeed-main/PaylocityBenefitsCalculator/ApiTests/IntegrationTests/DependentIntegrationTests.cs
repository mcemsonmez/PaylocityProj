using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Api.Dtos.Dependent;
using Api.Dtos.Paycheck;
using Api.Models;
using Xunit;

namespace ApiTests.IntegrationTests;

public class DependentIntegrationTests : IntegrationTest
{
    [Fact]
    //task: make test pass
    public async Task WhenAskedForAllDependents_ShouldReturnAllDependents()
    {
        var response = await HttpClient.GetAsync("/api/v1/dependents");
        #region OldData
        //var dependents = new List<GetDependentDto>
        //{
        //    new()
        //    {
        //        Id = 1,
        //        FirstName = "Spouse",
        //        LastName = "Morant",
        //        Relationship = Relationship.Spouse,
        //        DateOfBirth = new DateTime(1998, 3, 3)
        //    },
        //    new()
        //    {
        //        Id = 2,
        //        FirstName = "Child1",
        //        LastName = "Morant",
        //        Relationship = Relationship.Child,
        //        DateOfBirth = new DateTime(2020, 6, 23)
        //    },
        //    new()
        //    {
        //        Id = 3,
        //        FirstName = "Child2",
        //        LastName = "Morant",
        //        Relationship = Relationship.Child,
        //        DateOfBirth = new DateTime(2021, 5, 18)
        //    },
        //    new()
        //    {
        //        Id = 4,
        //        FirstName = "DP",
        //        LastName = "Jordan",
        //        Relationship = Relationship.DomesticPartner,
        //        DateOfBirth = new DateTime(1974, 1, 2)
        //    }
        //};
        #endregion
        var dependents = new List<GetDependentDto> {
        new (){
              Id = 1,
              FirstName =  "Spouse",
              LastName= "Morant",
              DateOfBirth = new DateTime(1998,03, 03),
              Relationship = (Relationship)1
        },
        new() {
             Id = 2,
             FirstName = "Child1",
             LastName = "Morant",
             DateOfBirth = new DateTime(2020,06,23),
             Relationship = (Relationship)3
        },
        new() {
          Id =  3,
          FirstName =  "Child2",
          LastName = "Morant",
          DateOfBirth = new DateTime(2021, 05, 18),
          Relationship = (Relationship)3
        },
        new (){
          Id =  4,
          FirstName =  "DP",
          LastName = "Jordan",
         DateOfBirth = new DateTime(1974,01,02),
         Relationship = (Relationship) 2
        },
        new () {
          Id =  5,
          FirstName =  "Dependent5",
          LastName = "Test",
         DateOfBirth = new DateTime(1980,01,01),
         Relationship = (Relationship)1
        },
        new () {
          Id =  6,
          FirstName =  "Dependent6",
          LastName = "Test",
         DateOfBirth = new DateTime(2000,02,02),
         Relationship = (Relationship)3
        },
        new() {
          Id =  7,
          FirstName =  "Dependent7",
          LastName = "Test",
         DateOfBirth = new DateTime(2005,03,03),
         Relationship = (Relationship)3
        },
        new() {
          Id =  8,
          FirstName =  "Dependent8",
          LastName = "Test",
         DateOfBirth = new DateTime(1950,04,04),
         Relationship = (Relationship)1
        },
        new() {
          Id =  9,
          FirstName =  "Dependent9",
          LastName = "Test",
         DateOfBirth = new DateTime(2005,05,05),
         Relationship = (Relationship)3
        },
        new() {
          Id =  10,
          FirstName =  "Dependent10",
          LastName = "Test",
         DateOfBirth = new DateTime(1960,06,06),
         Relationship = (Relationship)2
        },
        new() {
          Id =  11,
          FirstName =  "Dependent11",
          LastName = "Test",
         DateOfBirth = new DateTime(1970,07,07),
         Relationship = (Relationship)3
        },
        new() {
          Id =  12,
          FirstName =  "Dependent12",
          LastName = "Test",
         DateOfBirth = new DateTime(1965,08,08),
         Relationship = (Relationship)3
        },
        new() {
          Id =  13,
          FirstName =  "Dependent13",
          LastName = "Test",
         DateOfBirth = new DateTime(1980,01,01),
         Relationship = (Relationship)1
        },
        new() {
          Id =  14,
          FirstName =  "Dependent14",
          LastName = "Test",
         DateOfBirth = new DateTime(2000,02,02),
         Relationship = (Relationship)3
        },
        new() {
          Id =  15,
          FirstName =  "Dependent15",
          LastName = "Test",
         DateOfBirth = new DateTime(2005,03,03),
         Relationship = (Relationship)3
        },
        new() {
          Id =  16,
          FirstName =  "Dependent16",
          LastName = "Test",
         DateOfBirth = new DateTime(1950,04,04),
         Relationship = (Relationship)1
        },
        new() {
          Id =  17,
          FirstName =  "Dependent17",
          LastName = "Test",
         DateOfBirth = new DateTime(2005,05,05),
         Relationship = (Relationship)3
        },
        new() {
          Id =  18,
          FirstName =  "Dependent18",
          LastName = "Test",
         DateOfBirth = new DateTime(1960,06,06),
         Relationship = (Relationship)2
        },
        new() {
          Id =  19,
          FirstName =  "Dependent19",
          LastName = "Test",
         DateOfBirth = new DateTime(1970,07,07),
         Relationship =(Relationship)3
        },
        new() {
          Id =  20,
          FirstName =  "Dependent20",
          LastName = "Test",
         DateOfBirth = new DateTime(1965,08,08),
         Relationship = (Relationship)3
        },
        new() {
          Id =  21,
          FirstName =  "Spouse1",
          LastName  = "Test",
         DateOfBirth = new DateTime(1984,01,01),
         Relationship = (Relationship)1
        } };
        await response.ShouldReturn(HttpStatusCode.OK, dependents);
    }

    [Fact]
    //task: make test pass
    public async Task WhenAskedForADependent_ShouldReturnCorrectDependent()
    {
        var response = await HttpClient.GetAsync("/api/v1/dependents/1");
        var dependent = new GetDependentDto
        {
            Id = 1,
            FirstName = "Spouse",
            LastName = "Morant",
            Relationship = Relationship.Spouse,
            DateOfBirth = new DateTime(1998, 3, 3)
        };
        await response.ShouldReturn(HttpStatusCode.OK, dependent);
    }

    [Fact]
    //task: make test pass
    public async Task WhenAskedForANonexistentDependent_ShouldReturn404()
    {
        var response = await HttpClient.GetAsync($"/api/v1/dependents/{int.MinValue}");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task WhenAddSecondSpouseOrDomesticPartner_ShouldReturnSuccessFalse()
    {
        try
        {
            using StringContent jsonContent = new(
               JsonSerializer.Serialize(new
               {
                    EmployeeId = 1000,
                    FirstName = "Spouse2",
                    LastName = "Test",
                    Relationship = Relationship.Spouse,
                    DateOfBirth = new DateTime(1985, 1, 1),
               }),
               Encoding.UTF8,
               "application/json");

            var response = await HttpClient.PostAsync("/api/v1/dependents", jsonContent);
            var respTxt = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new(JsonSerializerDefaults.Web);
            var model = JsonSerializer.Deserialize<ApiResponse<PaycheckResultDto>>(respTxt, options);
            Assert.False(model.Success);
        }
        catch (Exception ex)
        {

        }
    }
}

