using System;
namespace Api.Models
{
	public class AppContants
	{
        public const int PaychecksPerYear = 26;
        public const decimal BaseCostPerMonth = 1000;
        public const decimal DependentCostPerMonth = 600;
        public const decimal AdditionalCostOver50 = 200;
        public const decimal HighIncomeThreshold = 80000;
        public const decimal AdditionalCostHighIncome = 0.02m;
    }
}

