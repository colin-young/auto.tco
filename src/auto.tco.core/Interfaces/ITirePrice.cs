namespace Auto.Tco.Core.Interfaces
{
    public interface ITirePrice
    {
        ITireData TireData { get; set; }

        double Price { get; set; }

        int MileageRating { get; set; }

        string Vendor { get; set; }

        string VendorLink { get; set; }
    }
}
