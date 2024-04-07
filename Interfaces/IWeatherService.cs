using WeatherApp.Models;

namespace WeatherApp.Interfaces
{
    public interface IWeatherService
    {
        public Task<WeatherResult> GetWeatherResult(string city);
        public Task<List<WeatherResult>> GetMinimumWeather();
        public Task<bool> UpdateCity(City city);
        public Task<bool> AddCity(City city);
        public Task<bool> DeleteCity(int CityID);
    }
}
