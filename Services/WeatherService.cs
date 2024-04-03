using WeatherApp.Interfaces;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly CityContext _citycontext;
        public WeatherService(CityContext citycontext) 
        {
            _citycontext = citycontext;
        }

        public async Task<List<WeatherResult>> GetMinimumWeather()
        {
            // implement this method to return the top 5 cities with minimnum weather.
            throw new NotImplementedException();
        }

        public async Task<WeatherResult> GetWeatherResult(string city)
        {
            // implement this method to get the Weather details of the given city.
            throw new NotImplementedException();
        }
        public async Task DeleteCity(int CityID)
        {
            // implment this method to delete the city from the DB.
            throw new NotImplementedException();
        }
        public async Task UpdateCity(City city)
        {
            // implement this method to update city details in DB
            throw new NotImplementedException();
        }
        public async Task AddCity(City city)
        {
            // implement this method to add a new city to the DB.
            throw new NotImplementedException();
        }
    }
}
