using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using Auto.Tco.Core;
using Auto.Tco.Core.Interfaces;
using Xunit;

namespace CarTCOCalculator.Tests
{
    public class PredictorTest
    {
        [Theory,
         InlineData(40000, 10000, 5, 90000),
         InlineData(0, 1, 5, 5)]
        public void TestEndOfLifeMileage(int start, int annual, int yearsOwned, int expected)
        {
            var personalData = A.Fake<IPersonalProfile>();
            personalData.LengthOfOwnership = yearsOwned;
            personalData.AnnualMileage = annual;

            var carData = A.Fake<ICarData>();
            carData.CurrentMileage = start;

            var financeDetails = A.Fake<IFinanceDetails>();

            var predictor = new Predictor(personalData, carData, financeDetails);

            predictor.EndOfLifeMileage.Should().Be(expected);
        }

        [Theory,
         InlineData(10000, 5000, 5000),
         InlineData(20000, 0, 20000),
         InlineData(10.0, 0.01, 9.99)]
        public void TestFinancedAmount(double purchase, double down, double expected)
        {
            var carData = A.Fake<ICarData>();
            carData.PurchasePrice = purchase;

            var financeDetails = A.Fake<IFinanceDetails>();
            financeDetails.Downpayment = down;

            var personalData = A.Fake<IPersonalProfile>();

            var predictor = new Predictor(personalData, carData, financeDetails);

            predictor.FinancedAmount.Should().BeApproximately(expected, 0.01f);
        }

        [Theory,
         InlineData(1200, new[] { 100.0, 150.0 }, new[] { 1200, 2400 }, 6.25)]
        public void TestTireAmount(int annualMileage, double[] tirePrices, int[] tireLife, double expected)
        {
            var carData = A.Fake<ICarData>();
            var priceList = new List<ITirePrice>();
            A.CallTo(() => carData.TirePrices).Returns(priceList);

            if (tirePrices.Any())
            {
                for (int i = 0; i < tirePrices.Length; i++)
                {
                    var price = A.Fake<ITirePrice>();
                    price.Price = tirePrices[i];
                    price.MileageRating = tireLife[i];

                    priceList.Add(price);
                }
            }

            var personalData = A.Fake<IPersonalProfile>();
            personalData.AnnualMileage = annualMileage;

            IFinanceDetails financeDetails = A.Fake<IFinanceDetails>();

            var predictor = new Predictor(personalData, carData, financeDetails);
            predictor.TireAmount.Should().BeApproximately(expected, 0.001);
        }

        [Theory,
         InlineData(100.0, 1200, 100.0, 1.00, 1200.0, new[] { 100.0, 150.0 }, new[] { 1200, 2400 }, 306.25), // all pricing = 100 + 100 + 100 + 6.25 = 306.25
         InlineData(100.0, 1200, 100.0, 1.00, 0.0, new double[] { }, new int[] { }, 201.0),                  // only gas = 100 + 0 + 100 + 0 = 201
         InlineData(100.0, 1200, 100.0, 1.00, 1200.0, new double[] { }, new int[] { }, 301.0)                // insurance + gas = 100 + 100 + 100 + 0 = 301
         InlineData(100.0, 1200, 100.0, 1.00, 0.0, new[] { 100.0, 150.0 }, new[] { 1200, 2400 }, 206.25),    // tires + gas = 100 + 0 + 100 + 6.25 = 206.25
         InlineData(100.0, 1200, 100.0, 0.0, 1200.0, new[] { 100.0, 150.0 }, new[] { 1200, 2400 }, 206.25)   // tires + insurance = 100 + 100 + 0 + 6.25 = 206.25
        ]
        public void TestMonthlyCost(double monthlyPayment, int annualMileage, double fuelEconomy, double gasPrice, double annualInsurance, double[] tirePrices, int[] tireLife, double expected)
        {
            var carData = A.Fake<ICarData>();
            var priceList = new List<ITirePrice>();
            A.CallTo(() => carData.TirePrices).Returns(priceList);

            if (tirePrices.Any())
            {
                for (int i = 0; i < tirePrices.Length; i++)
                {
                    var price = A.Fake<ITirePrice>();
                    price.Price = tirePrices[i];
                    price.MileageRating = tireLife[i];

                    priceList.Add(price);
                }
            }

            carData.FuelEconomy = fuelEconomy;

            var personalData = A.Fake<IPersonalProfile>();
            personalData.AnnualMileage = annualMileage;
            personalData.GasPrice = gasPrice;

            IFinanceDetails financeDetails = A.Fake<IFinanceDetails>();
            A.CallTo(() => financeDetails.MonthlyPayment(A<double>.Ignored)).Returns(monthlyPayment);

            var predictor = new Predictor(personalData, carData, financeDetails);
            predictor.MonthlyPayment.Should().BeApproximately(expected, 0.001);
        }
    }
}