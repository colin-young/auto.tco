using System.Linq;
using Auto.Tco.Core.Interfaces;

namespace Auto.Tco.Core
{
    public class Predictor
    {
        IPersonalProfile _personalProfile;
        IFinanceDetails _financeDetails;
        ICarData _carData;

        public Predictor(IPersonalProfile personalProfile, ICarData carData, IFinanceDetails financeData)
        {
            _personalProfile = personalProfile;
            _carData = carData;
            _financeDetails = financeData;
        }

        // calculated properties
        public int EndOfLifeMileage => _carData.CurrentMileage + (_personalProfile.AnnualMileage * _personalProfile.LengthOfOwnership);
        public double FinancedAmount => _carData.PurchasePrice - _financeDetails.Downpayment;
        public double MonthlyPayment
        {
            get
            {
                return GasAmount +
                       _financeDetails.MonthlyPayment(FinancedAmount) +
                       _carData.AnnualInsurance / 12.0 +
                       TireAmount;
                }
        }

        public double GasAmount => _personalProfile.AnnualMileage / 100.0 * _carData.FuelEconomy
                                * _personalProfile.GasPrice / 12.0;

        public double TireAmount => _carData.TirePrices.Any()
                                 ? _carData.TirePrices.Select(p => p.Price / p.MileageRating)?.Min()  * _personalProfile.AnnualMileage / 12 ?? 0.0
                                 : 0.0;
    }
}
