using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly Serilog.ILogger _logger;

        private readonly IValidator<WeatherForecastDto> _validator;

        public WeatherForecastController(Serilog.ILogger logger, IValidator<WeatherForecastDto> validator)
        {
            _logger = logger;
            _validator = validator;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            //FLUENT API VALIDATION
            var dtoToBeValidated = new WeatherForecastDto();
            var results = _validator.Validate(dtoToBeValidated, ruleSet: "all");

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray();
        }
    }
}
