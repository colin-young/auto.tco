using FluentAssertions;
using SharedLibrary;
using Xunit;

namespace CarTCOCalculator.Tests
{
    
    public class CarDataTest
	{
        [Theory,
         InlineData(new double[] { 6.0f, 1.0f }, 7.0f),
         InlineData(new double[] { }, 0.0),
         InlineData(new double[] { 1.0f }, 1.0f)]
        public void TestTotalTaxRate(double[] taxes, double expected)
        {
            var basicData = new BasicData();

            foreach (var localTax in taxes)
            {
                basicData.LocalTaxes.Add(localTax);
            }

            basicData.TaxRate.Should().BeApproximately(expected, 0.001f);
        }

        [Theory, 
         InlineData(40000, 10000, 5, 90000),
         InlineData(0, 1, 5, 5)]
        public void TestEndOfLifeMileage(int start, int annual, int yearsOwned, int expected)
        {
            var basicData = new BasicData
            {
                AnnualMileage = annual,
                LengthOfOwnership = yearsOwned
            };

            var carData = new CarData(basicData)
            {
                CurrentMileage = start
            };

            carData.EndOfLifeMileage.Should().Be(expected);
        }

        [Theory,
         InlineData(10000, 5000, 5000),
         InlineData(20000, 0, 20000),
         InlineData(10.0, 0.01, 9.99)]
        public void TestFinancedAmount(double purchase, double down, double expected)
        {
            var basicData = new BasicData
            {
                Downpayment = down,
            };

            var carData = new CarData(basicData)
            {
                PurchasePrice = purchase
            };

            carData.FinancedAmount.Should().BeApproximately(expected, 0.01f);
        }

        [Theory,
         InlineData(10000, 0.05, 60, 188.71),
         InlineData(10000, 0.12, 12, 888.49)]
        public void TestPaymentCalculation(double amount, double rate, int terms, double expected)
        {
            var basicData = new BasicData
            {
                Downpayment = 0,
                AnnualFinanceRate = rate,
                NumberOfPayments = terms
            };

            var carData = new CarData(basicData)
            {
                PurchasePrice = amount
            };

            carData.FinancedAmount.Should().BeApproximately(amount, 0.004f);
            carData.MonthlyPayment.Should().BeApproximately(expected, 0.004f);
        }
	}
}