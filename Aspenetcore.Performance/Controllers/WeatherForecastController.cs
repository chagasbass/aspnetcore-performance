using Aspnetcore.Performance.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aspnetcore.Performance.Controllers
{
    [ApiController]
    [Route("v1/weathers")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private IEnumerable<WeatherForecast> _weatherForecasts;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            CreateWeathers();
        }

        private void CreateWeathers()
        {
            var rng = new Random();
            _weatherForecasts = Enumerable.Range(1, 100).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToList();
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get() => _weatherForecasts;

        [HttpGet("{id}")]
        public WeatherForecast Get(ShortGuid id) => _weatherForecasts.FirstOrDefault(x => x.Id == id);
    }
}
