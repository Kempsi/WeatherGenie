namespace WeatherScanner.Entities.WeatherModels
{
    public class WeatherData
    {
        public string? Main { get; set; }
        public string? City { get; set; }
        public double Temp { get; set; }
        public double TempFeelsLike { get; set; }
        public string? Description { get; set; }
    }
}
