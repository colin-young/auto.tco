using System;

namespace SharedLibrary
{
    public interface ITireData
    {
        string VehicleClass { get; set; }
        int Width { get; set; }
        int AspectRatio { get; set; }
        string TireType { get; set; }
        int Diameter { get; set; }
        int LoadIndex { get; set; }
        string SpeedRating { get; set; }
    }

    public class TireData : ITireData
    {
        public string VehicleClass { get; set; }
        public int Width { get; set; }
        public int AspectRatio { get; set; }
        public string TireType { get; set; }
        public int Diameter { get; set; }
        public int LoadIndex { get; set; }
        public string SpeedRating { get; set; }

        const string tireParseRegEx = @"(P|LT)?([0-9]{3})/([0-9]{2,3})([BDR]{1})([0-9]{2})\s([0-9]{2})(\(?[A-Z]?\)?[0-9]?)";

        public TireData(string vehicleClass, int width, int aspectRatio, string tireType, int diameter, int loadIndex, string speedRating)
        {
            VehicleClass = vehicleClass;
            Width = width;
            AspectRatio = aspectRatio;
            TireType = tireType;
            Diameter = diameter;
            LoadIndex = loadIndex;
            SpeedRating = speedRating;
        }

        public override string ToString()
        {
            return $"{VehicleClass}{Width}/{AspectRatio}{TireType}{Diameter} {LoadIndex}{SpeedRating}";
        }
    }
}