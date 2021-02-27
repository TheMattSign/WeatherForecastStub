using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TestWebApplication.Models;
using TestWebApplication.Services;

namespace TestWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : Controller
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherForecastService _weatherForecastService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
        }

        public IActionResult Index()
        {
            return View(GetList());
        }

        [HttpGet("List")]
        public IEnumerable<WeatherForecast> GetList()
        {
            return _weatherForecastService.Get();
        }

        [HttpGet("{id}")]
        public WeatherForecast GetById(Guid id)
        {
            return _weatherForecastService.GetById(id);
        }

        [HttpPost]
        public ActionResult<WeatherForecast> Post(WeatherForecast weatherForecast)
        {
            if (weatherForecast != null)
            {
                weatherForecast = _weatherForecastService.Add(weatherForecast);
                return CreatedAtAction(nameof(GetById), new { weatherForecast.id }, weatherForecast);
            }

            return null;
        }

        [HttpPut("{id}")]
        public void Put(Guid id, WeatherForecast weatherForecast)
        {
            _weatherForecastService.Update(id, weatherForecast);
        }
    }
}
