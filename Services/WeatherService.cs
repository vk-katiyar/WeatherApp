using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
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
            List<WeatherResult> result = null;
            try
            {
                var cityList = await _citycontext.City.AsQueryable().ToListAsync();
                foreach(var city in cityList)
                {
                    result.Add(await GetCityWeatherDetails(city));
                }
                if (result != null)
                {
                    result = result.OrderBy(x => x.Temperature).ToList();
                    result = result.Take(5).Select(item => new WeatherResult
                    {
                        Temperature = item.Temperature,
                        CityName = item.CityName,
                        WindDirection = item.WindDirection,
                        WindSpeed = item.WindSpeed
                    }).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        public async Task<WeatherResult> GetWeatherResult(string city)
        {
            WeatherResult weatherResult = null;
            try
            {
                if(!string.IsNullOrEmpty(city))
                {
                    var cityDetails = _citycontext.City.FirstOrDefault(x => x.CityName.ToLower() == city.ToLower());
                    if(cityDetails != null)
                    {
                        weatherResult = await GetCityWeatherDetails(cityDetails);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return weatherResult;
        }
        public async Task<WeatherResult> GetCityWeatherDetails(City city)
        {
            WeatherResult weatherResult = null;
            HttpClient client = new HttpClient();
            try
            {
                var latitude = city.Longitude;
                var longitude = city.Longitude;

                string URL = "https://api.open-meteo.com/v1/forecast";
                string urlParameters = "?latitude=" + latitude + "&longitude=" + longitude + "&current_weather=true";

                client.BaseAddress = new Uri(URL);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(urlParameters).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Response data = JsonConvert.DeserializeObject<Response>(result);
                    weatherResult = new WeatherResult
                    {
                        CityName = city.CityName,
                        Temperature = data.current_weather.temperature,
                        WindDirection = data.current_weather.winddirection,
                        WindSpeed = data.current_weather.windspeed
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                client.Dispose();
            }
            return weatherResult;
        }
        public async Task<bool> DeleteCity(int CityID)
        {
            bool IsDeleted = false;
            try
            {
                var city = await _citycontext.City.AsQueryable().FirstOrDefaultAsync(x => x.CityID == CityID);
                if (city != null)
                {
                    _citycontext.City.Remove(city);
                }
                await _citycontext.SaveChangesAsync();
                IsDeleted = true;
            }
            catch (Exception)
            {
                IsDeleted = false;
                throw;
            }
            return IsDeleted;
        }
        public async Task<bool> UpdateCity(City city)
        {
            bool IsUpdated = false;
            try
            {
                if (city != null)
                {
                    var cityDetails = _citycontext.City.AsQueryable().FirstOrDefault(x => x.CityID == city.CityID);
                    if (cityDetails != null)
                    {
                        cityDetails.Longitude = city.Longitude;
                        cityDetails.Latitude = city.Latitude;
                        cityDetails.CityName = city.CityName;

                        _citycontext.City.Update(cityDetails);
                        await _citycontext.SaveChangesAsync();
                        IsUpdated = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsUpdated;
        }
        public async Task<bool> AddCity(City city)
        {
            bool IsAdded = false;
            try
            {
                if(city != null)
                {
                    await _citycontext.City.AddAsync(city);
                    await _citycontext.SaveChangesAsync();
                    IsAdded = true;
                }
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            return IsAdded;
        }
    }
}
