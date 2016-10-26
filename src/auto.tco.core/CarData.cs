using System;

namespace SharedLibrary
{
    public class CarData
    {
        readonly IBasicData _basicData;

        public CarData(IBasicData basicData)
        {
            _basicData = basicData;
        }

        public int CurrentMileage { get; set; }
        public double PurchasePrice { get; set; }

        // calculated properties
        public int EndOfLifeMileage => CurrentMileage + (_basicData.AnnualMileage * _basicData.LengthOfOwnership);
        public double FinancedAmount => PurchasePrice - _basicData.Downpayment;
        public double MonthlyPayment
        {
            get
            {
                var periodRate = _basicData.AnnualFinanceRate / 12.0f;
                var denominator = Math.Pow(1 + periodRate, _basicData.NumberOfPayments);

                return (FinancedAmount * periodRate) / (1 - 1 / denominator);
            }
        }
    }
}