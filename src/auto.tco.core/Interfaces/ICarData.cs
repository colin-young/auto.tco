using System.Collections.Generic;

namespace Auto.Tco.Core.Interfaces
{
    public interface ICarData
    {
        ITireData TireType { get; set; }

        IList<ITirePrice> TirePrices { get; }

        double PurchasePrice { get; set; }

        int CurrentMileage { get; set; }

        double FuelEconomy { get; set; }

        double AnnualInsurance { get; set; }

        int ModelYear { get; set; }

        string Manufacturer { get; set; }

        string Model { get; set; }

        string SubModel { get; set; }
    }
}