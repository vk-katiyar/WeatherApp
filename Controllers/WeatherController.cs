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
        public async Task<ActionResult<WeatherResult>> GetWeatherByCity([FromQuery] string city)
        {
            try
            {
                if (city != null)
                {
                    var cityDetails = await _weatherService.GetWeatherResult(city);
                    if (cityDetails != null)
                    {
                        return Ok(cityDetails);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetMinimumWeather")]
        public async Task<ActionResult<List<WeatherResult>>> GetMinimumWeather()
        {
            try
            {
                var cityDetails = await _weatherService.GetMinimumWeather();
                if (cityDetails != null)
                {
                    return Ok(cityDetails);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("UpdateCityDetails")]
        public async Task<IActionResult> UpdateCityDetails([FromBody] City city)
        {
            try
            {
                if (city != null)
                {
                    var IsUpdated = await _weatherService.UpdateCity(city);
                    if (IsUpdated)
                    {
                        return NoContent();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("AddCity")]
        public async Task<IActionResult> AddCity([FromBody] City city)
        {
            try
            {
                if (city != null)
                {
                    var IsAdded = await _weatherService.AddCity(city);
                    if (IsAdded)
                    {
                        return NoContent();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else { return BadRequest(); }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{CityID:int}")]
        [Route("DeleteCity")]
        public async Task<IActionResult> DeleteCity(int CityID)
        {
            try
            {
                var IsDeleted = await _weatherService.DeleteCity(CityID);
                if (IsDeleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
