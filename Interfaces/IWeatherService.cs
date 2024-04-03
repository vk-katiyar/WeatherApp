using WeatherApp.Models;

namespace WeatherApp.Interfaces
{
    public interface IWeatherService
    {
        public Task<WeatherResult> GetWeatherResult(string city);
        public Task<List<WeatherResult>> GetMinimumWeather();
        public Task UpdateCity(City city);
        public Task AddCity(City city);
        public Task DeleteCity(int CityID);
    }
}
