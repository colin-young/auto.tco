using System.Collections.Generic;
using Auto.Tco.Core.Interfaces;

namespace SharedLibrary
{
    public class CarData
    {
        IList<ITirePrice> _tirePrices = new List<ITirePrice>();

        public CarData() { }

        public double AnnualInsurance { get; set; }

        public int CurrentMileage { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public int ModelYear { get; set; }

        public double PurchasePrice { get; set; }

        public string SubModel { get; set; }

        public ITireData TireType { get; set; }

        public IList<ITirePrice> TirePrices => _tirePrices;

        // in vol/100 distance
        public double FuelEconomy { get; set; }
    }
}