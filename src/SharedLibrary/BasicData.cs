using System.Collections.Generic;
using System.Linq;

namespace SharedLibrary
{
    public class BasicData : IBasicData
    {
        public int AnnualMileage { get; set; }
        public int LengthOfOwnership { get; set; }

        // financial
        public double AnnualFinanceRate { get; set; }
        public int NumberOfPayments { get; set; }
        public IList<double> LocalTaxes { get; }
        public double GasPrice { get; set; }
        public double Downpayment { get; set; }

        // calculated financial properties
        public double TaxRate => LocalTaxes?.Sum() ?? 0.0f;

        public BasicData()
		{
            LocalTaxes = new List<double>();
		}
	}
}