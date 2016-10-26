using System.Collections.Generic;

namespace SharedLibrary
{
    public interface IBasicData
    {
        int AnnualMileage { get; set; }
        int LengthOfOwnership { get; set; }
        double AnnualFinanceRate { get; set; }
        int NumberOfPayments { get; set; }
        IList<double> LocalTaxes { get; }
        double GasPrice { get; set; }
        double Downpayment { get; set; }
        double TaxRate { get; }
    }
    
}