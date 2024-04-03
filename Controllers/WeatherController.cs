using Microsoft.AspNetCore.Mvc;
using WeatherApp.Interfaces;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        [Route("GetWeatherByCity")]
        public async Task<ActionResult<WeatherResult>> GetWeatherByCity([FromQuery]string city)
        {
            
        }

        [HttpGet]
        [Route("GetMinimumWeather")]
        public async Task<ActionResult<List<WeatherResult>>> GetMinimumWeather()
        {

        }

        [HttpPut]
        [Route("UpdateCityDetails")]
        public async Task<IActionResult> UpdateCityDetails([FromBody]City city)
        {

        }

        [HttpPost]
        [Route("AddCity")]
        public async Task<IActionResult> AddCity([FromBody]City city)
        {

        }

        [HttpDelete("{CityID:int}")]
        [Route("DeleteCity")]
        public async Task<IActionResult> DeleteCity(int CityID)
        {

        }
    }
}
