using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Api.Dtos.Paycheck;
using Api.Models;
using Xunit;
namespace ApiTests.IntegrationTests
{
    public class PaycheckIntegrationTests: IntegrationTest
    {
        [Theory]
        [InlineData("An employee with one dependent who is under 50 years old.", 4, 1, 2373.077)]
        [InlineData("An employee with multiple dependents who are all under 50 years old.", 5, 1, 2192.308)]
        [InlineData("An employee with one dependent who is over 50 years old.", 6, 1, 2540.769)]
        [InlineData("An employee with multiple dependents, including one over 50 years old.", 7, 1, 2322.308)]
        [InlineData("An employee with multiple dependents, including multiple dependents over 50 years old.", 8, 1, 2565.385)]

        [InlineData("An employee with one dependent who is under 50 years old.(Under 80k)", 9, 1, 1669.231)]
        [InlineData("An employee with multiple dependents who are all under 50 years old.(Under 80k)", 10, 1,  1492.308)]
        [InlineData("An employee with one dependent who is over 50 years old.(Under 80k)", 11, 1, 1838.462)]
        [InlineData("An employee with multiple dependents, including one over 50 years old.(Under 80k)", 12, 1, 1623.077)]
        [InlineData("An employee with multiple dependents, including multiple dependents over 50 years old.(Under 80k)", 13, 1, 1869.231)]
        public async Task WhenGetPaycheck_ShouldReturnAmount(string exp, int employeeId, int payhcheckTerm, decimal amount)
        {
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    EmployeeId = employeeId,
                    PaycheckNumber = payhcheckTerm
                }),
                Encoding.UTF8,
                "application/json");

            var response = await HttpClient.PostAsync("/api/v1/paycheck", jsonContent);
            var respTxt = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new(JsonSerializerDefaults.Web);
            var model = JsonSerializer.Deserialize<ApiResponse<PaycheckResultDto>>(respTxt, options);
            Assert.True(model.Success);
            var roundedAmount = decimal.Round(model.Data.Amount, 3);
            Assert.Equal(roundedAmount, amount);
        }

	}
}

