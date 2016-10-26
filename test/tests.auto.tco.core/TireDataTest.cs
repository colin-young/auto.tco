using FluentAssertions;
using SharedLibrary;
using Xunit;

namespace CarTCOCalculator.Tests
{
    public class TireDataTest
    {
        [Theory,
         InlineData("P", 195, 55, "R", 16, 85, "H", "P195/55R16 85H"),
         InlineData(null, 195, 55, null, 16, 85, "H", "195/5516 85H")]
                    public void TestTireData(string vehicleClass, 
                                             int width, 
                                             int aspectRatio, 
                                             string tireType, 
                                             int diameter, 
                                             int load, 
                                             string speed, 
                                             string expected)
        {
            var tireData = new TireData(vehicleClass, width, aspectRatio, tireType, diameter, load, speed);
            var display = tireData.ToString();

            display.Should().Be(expected);
        }
    }
}