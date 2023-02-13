using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecast1Controller : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecast1Controller> _logger;

        public WeatherForecast1Controller(ILogger<WeatherForecast1Controller> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast1")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}