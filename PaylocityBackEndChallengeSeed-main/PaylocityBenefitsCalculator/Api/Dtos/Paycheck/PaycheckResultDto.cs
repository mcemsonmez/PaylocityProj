using System;
namespace Api.Dtos.Paycheck
{
	public class PaycheckResultDto
	{
		public int EmployeeId { get; set; }
		public int PaycheckNumber { get; set; }
		public decimal Amount { get; set; }
	}
}

